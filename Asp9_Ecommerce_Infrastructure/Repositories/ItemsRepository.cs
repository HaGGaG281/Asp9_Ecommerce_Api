using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using Asp9_Ecommerce_Core.Interfaces;
using Asp9_Ecommerce_Core.Models;
using Asp9_Ecommerce_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Asp9_Ecommerce_Core.DTOs.Orders;
using Mapster;
using Asp9_Ecommerce_Core.Mapping_Profiles;

namespace Asp9_Ecommerce_Infrastructure.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly AppDbContext context;
        //private readonly IMapper mapper;

        public ItemsRepository(AppDbContext context  /*IMapper mapper*/)
        {
            this.context = context;
            //this.mapper = mapper;
        }

        ///// <summary>
        ////Return anunymus object new{} to retrieve the store name and unitcode  ,, 
        /// لو انا هحدد الحاجات اللي عاوزها من الريليتد داتا مش هستخدم الاوتومابر
        public async Task<IEnumerable<Item_dto>> GetItemsWithUnitsAsync()
        {
            // استخدام الإعداد المحمل مسبقاً
            var config = Mapping_Profile.Config;

            var items = await context.Items
                                     .ProjectToType<Item_dto>(config)
                                     .ToListAsync();

            return items;
        }






        ///// <summary>
        ////Return every thing of store ,, but i need to retrieve the store name only
        //public async Task<IEnumerable<Item_dto>> GetItemsWithUnitsAsync()
        //{
        //    var items = await context.Items
        //        .Include(x => x.ItemsUnits) // Include ItemsUnits relationship
        //        .Include(x => x.ItemsStores) // Include ItemsStores relationship
        //            .ThenInclude(store => store.Stores.name) // Include Stores relationship
        //        .ToListAsync();

        //    return mapper.Map<IEnumerable<Item_dto>>(items); // Map entities to DTOs
        //}


        ///// <summary>
        ////Return manually mapping ,, not using AutoMapper

        //public async Task<IEnumerable<Item_dto>> GetItemsWithUnitsAsync()
        //{
        //    var items = await context.Items
        //    .Include(x => x.ItemsUnits)
        //    .Select(x => new Item_dto
        //    {
        //        Id = x.Id,
        //        name = x.name,
        //        ItemsUnits = x.ItemsUnits.Select(unit => unit.UnitCode).ToList()
        //    })
        //    .ToListAsync();

        //    return items;
        //}


        public async Task<string> AddBulkQuantityofItemToCartAsync(AddToCartDto dto, int userId)
        {
            // التأكد من أن العنصر موجود في المتجر
            var item = await context.Items.FindAsync(dto.ItemCode);
            var store = await context.Stores.FindAsync(dto.StoreId);

            if (item == null || store == null)
            {
                return "Item or store not found.";
            }

            // التحقق من وجود العنصر في العربة مسبقًا
            var existingItem = await context.ShoppingCartItem
                .FirstOrDefaultAsync(x => x.Cus_Id == userId && x.ItemCode == dto.ItemCode && x.Store_Id == dto.StoreId);

            if (existingItem != null)
            {
                // إذا كان العنصر موجودًا بالفعل في العربة، نقوم بتحديث الكمية+1
                if (dto.Quantity > 0)
                    existingItem.Quantity = dto.Quantity;
                else
                    existingItem.Quantity += 1;

                existingItem.U_Code = dto.U_Code;
                existingItem.Store_Id = dto.StoreId;
                existingItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                // إذا لم يكن العنصر موجودًا، نقوم بإنشاء عنصر جديد في العربة
                var shoppingCartItem = new ShoppingCartItem
                {
                    Cus_Id = userId,
                    ItemCode = dto.ItemCode,
                    Store_Id = dto.StoreId,
                    Quantity = dto.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Factor = 1, // القيمة يمكن أن تتغير حسب ما تحتاجه
                    U_Code = dto.U_Code
                };

                context.ShoppingCartItem.Add(shoppingCartItem);
            }

            await context.SaveChangesAsync();

            return "Item added to cart successfully.";
        }
        


         public async Task<string> AddOneQuantityofIteToCartAsync(AddToCartDto dto, int userId)
         {
            // التأكد من أن العنصر موجود في المتجر
            var item = await context.Items.FindAsync(dto.ItemCode);
            var store = await context.Stores.FindAsync(dto.StoreId);

            if (item == null || store == null)
            {
                return "Item or store not found.";
            }

            // التحقق من وجود العنصر في العربة مسبقًا
            var existingItem = await context.ShoppingCartItem
                .FirstOrDefaultAsync(x => x.Cus_Id == userId && x.ItemCode == dto.ItemCode && x.Store_Id == dto.StoreId);

            if (existingItem != null)
            {
                // إذا كان العنصر موجودًا بالفعل في العربة، نقوم بتحديث الكمية+1
                if(dto.Quantity >0)
                    existingItem.Quantity +=1 ;
                else
                    existingItem.Quantity += dto.Quantity;


                existingItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                // إذا لم يكن العنصر موجودًا، نقوم بإنشاء عنصر جديد في العربة
                var shoppingCartItem = new ShoppingCartItem
                {
                    Cus_Id = userId,
                    ItemCode = dto.ItemCode,
                    Store_Id = dto.StoreId,
                    Quantity = dto.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Factor = 1, // القيمة يمكن أن تتغير حسب ما تحتاجه
                    U_Code = dto.U_Code
                };

                context.ShoppingCartItem.Add(shoppingCartItem);
            }

            await context.SaveChangesAsync();

            return "Item added to cart successfully.";
        }

    }

}



