using Asp9_Ecommerce_Core.DTOs.CartDTO;
using Asp9_Ecommerce_Core.DTOs.CustomersDTO;
using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using Asp9_Ecommerce_Core.Interfaces;
using Asp9_Ecommerce_Core.Models;
using Asp9_Ecommerce_Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CustomersRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<InvoiceReceiptDto> CalculateInvoiceTotalAsync(int invoiceId, int customerId)
        {
            // البحث عن الفاتورة وتأكيد أنها تخص العميل
            var invoice = await context.Invoices
                .Include(i => i.InvoiceDetail)
                .ThenInclude(d => d.Items)
                .FirstOrDefaultAsync(i => i.Id == invoiceId && i.Cus_Id == customerId);

            if (invoice == null)
            {
                return null; // أو يمكنك إعادة رسالة مثل "Invoice not found"
            }

            double totalPrice = 0;

            // حساب السعر الإجمالي لكل عنصر
            foreach (var detail in invoice.InvoiceDetail)
            {
                double itemTotalPrice = (detail.Quantity * detail.Price) / detail.Factor;
                totalPrice += itemTotalPrice;
            }

            // تحديث السعر الإجمالي في الفاتورة
            invoice.NetPrice = totalPrice;
            await context.SaveChangesAsync();

            // إعداد الإيصال للعميل
            var receipt = new InvoiceReceiptDto
            {
                InvoiceId = invoice.Id,
                CustomerId = invoice.Cus_Id,
                CreatedAt = invoice.CreatedAt,
                TotalPrice = totalPrice,
                Items = invoice.InvoiceDetail.Select(d => new InvoiceItemDto
                {
                    ItemName = d.Items.name,
                    Quantity = d.Quantity,
                    UnitName = context.Units.FirstOrDefault(u => u.Id == d.Unit_Code)?.Name ?? "Unknown",
                    PricePerUnit = d.Price,
                    TotalPrice = (d.Quantity * d.Price) / d.Factor
                }).ToList()
            };

            return receipt;
        }

        public async Task<string> CreateInvoiceAsync(int customerId)
        {
            // جلب العناصر الموجودة في عربة التسوق للعميل
            var cartItems = await context.ShoppingCartItem
                .Include(ci => ci.Items)
                .Where(ci => ci.Cus_Id == customerId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return "No items in the cart to create an invoice.";
            }

            // إنشاء قائمة لتخزين المنتجات التي لا يمكن شراؤها
            var unavailableItems = new List<string>();
            double totalNetPrice = 0;

            // إنشاء رأس الفاتورة
            var invoice = new Invoices
            {
                Cus_Id = customerId,
                CreatedAt = DateTime.Now,
                NetPrice = 0, // سيتم حسابها لاحقًا
                Transaction_Type = 1, // نوع المعاملة (يمكن تعديله حسب الحاجة)
                Payment_Type = 1, // نوع الدفع (يمكن تعديله حسب الحاجة)
                is_Posted = true,
                is_Closed = false
            };

            context.Invoices.Add(invoice);
            await context.SaveChangesAsync();

            // إضافة تفاصيل الفاتورة
            foreach (var cartItem in cartItems)
            {
                // جلب العنصر من جدول ItemsStores
                var itemStore = await context.ItemsStores
                    .FirstOrDefaultAsync(i => i.ItemId == cartItem.ItemCode && i.StoreId == cartItem.Store_Id);

                if (itemStore == null)
                {
                    unavailableItems.Add(cartItem.Items.name); // المنتج غير موجود
                    continue;
                }

                // حساب الكمية المتاحة
                double availableQuantity = itemStore.Balance - itemStore.ReservedQuantity;

                // التحقق إذا كانت الكمية المطلوبة أكبر من الكمية المتاحة
                if (cartItem.Quantity > availableQuantity)
                {
                    // إضافة المنتج للقائمة لو الكمية غير متوفرة
                    unavailableItems.Add(cartItem.Items.name); // اسم المنتج
                    continue; // تجاهل هذا المنتج واستمر مع المنتجات الأخرى
                }

                // حساب السعر لكل عنصر بناءً على الوحدة والكمية والعامل
                double unitPrice = cartItem.Items.price; // السعر الأساسي للوحدة الأساسية
                double itemTotalPrice = (cartItem.Quantity * unitPrice) / cartItem.Factor; // إجمالي السعر لهذا العنصر
                totalNetPrice += itemTotalPrice;

                // إضافة تفاصيل العنصر في الفاتورة
                var invoiceDetail = new InvoiceDetail
                {
                    InvoiceId = invoice.Id,
                    ItemCode = cartItem.ItemCode,
                    Quantity = cartItem.Quantity,
                    Factor = cartItem.Factor,
                    Price = (int)unitPrice,
                    Unit_Code = cartItem.U_Code,
                    CreatedAt = DateTime.Now
                };

                context.InvoiceDetail.Add(invoiceDetail);

                // تحديث الكمية المحجوزة
                itemStore.ReservedQuantity += cartItem.Quantity;
                context.ItemsStores.Update(itemStore);
            }

            // تحديث السعر الإجمالي للفاتورة
            invoice.NetPrice = totalNetPrice;

            // إزالة المنتجات التي تم شراؤها فقط من عربة التسوق
            context.ShoppingCartItem.RemoveRange(
                cartItems.Where(ci => !unavailableItems.Contains(ci.Items.name))
            );

            // حفظ التغييرات في قاعدة البيانات
            await context.SaveChangesAsync();

            // إنشاء رسالة توضيحية للمنتجات التي لم تتم
            // إضافة العناصر غير المتوفرة مع الكمية المتاحة
            if (unavailableItems.Any())
            {
                var unavailableItemsMessage = string.Join(", ", unavailableItems.Select(item =>
                {
                    // جلب أول عنصر في cartItems يتوافق مع item
                    var cartItem = cartItems.FirstOrDefault(i => i.Items.name == item);

                    if (cartItem != null)
                    {
                        // جلب الكمية المتاحة لهذا المنتج
                        var itemStore = context.ItemsStores
                            .FirstOrDefault(i => i.ItemId == cartItem.ItemCode);

                        // إذا تم العثور على المنتج في المخزن، إرجاع اسم المنتج مع الكمية المتاحة
                        if (itemStore != null)
                        {
                            double availableQuantity = itemStore.Balance - itemStore.ReservedQuantity;
                            return $"{item} (Available Quantity: {availableQuantity})";
                        }
                    }

                    return item; // في حالة عدم وجود المنتج في cartItems أو في المخزن
                }));

                return $"Invoice created successfully with ID: {invoice.Id} and Total Price: {totalNetPrice}. However, the following items were unavailable: {unavailableItemsMessage}.";
            }



            return $"Invoice created successfully with ID: {invoice.Id} and Total Price: {totalNetPrice}.";
        }

        public async Task<IEnumerable<CartItemsDTO>> GetAllCartItemsAsync(int customerId)
        {
            // جلب الـ items الموجودة في سلة العميل باستخدام Cus_Id
            var cartItems = await context.ShoppingCartItem
                                          .Where(ci => ci.Cus_Id == customerId)
                                          .Include(ci => ci.Items) // Include Items لكي نتمكن من الحصول على تفاصيل الـ item
                                          .Include(ci => ci.Items.ItemsUnits) // Include ItemsUnits لكي نتمكن من الحصول على الـ units
                                          .ThenInclude(iu => iu.Units) // Include الـ Units لكي نحصل على اسم الوحدة
                                          .ToListAsync();

            // تحويل الـ cartItems لـ Item_dto
            var itemsDto = cartItems.Select(ci => new CartItemsDTO
            {
                name = ci.Items.name,
                description = ci.Items.description,
                price = ci.Items.price,
                ItemUnit = ci.Items.ItemsUnits
                                    .Where(unit => unit.UnitCode == ci.U_Code) // نفلتر للوحدة المطلوبة بناءً على U_Code
                                    .Select(unit => unit.Units.Name) // نرجع اسم الوحدة
                                    .FirstOrDefault() // نأخذ أول اسم وحدة من القائمة
            }).ToList();

            return itemsDto;
        }




        public Task<IEnumerable<Item_dto>> GetItemsWithUnitsAsync()
        {
            throw new NotImplementedException();
        }
    }

}
