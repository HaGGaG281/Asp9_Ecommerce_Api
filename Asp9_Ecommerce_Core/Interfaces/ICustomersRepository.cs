using Asp9_Ecommerce_Core.DTOs.CartDTO;
using Asp9_Ecommerce_Core.DTOs.CustomersDTO;
using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Interfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Item_dto>> GetItemsWithUnitsAsync();
        Task<IEnumerable<CartItemsDTO>> GetAllCartItemsAsync(int customerId);

        Task<InvoiceReceiptDto> CalculateInvoiceTotalAsync(int invoiceId, int customerId);
        Task<string> CreateInvoiceAsync( int customerId);


    }
}
