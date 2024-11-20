using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using Asp9_Ecommerce_Core.DTOs.Orders;
using Asp9_Ecommerce_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Interfaces
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item_dto>> GetItemsWithUnitsAsync();
        Task<string> AddBulkQuantityofItemToCartAsync(AddToCartDto dto, int userId);
        Task<string> AddOneQuantityofIteToCartAsync(AddToCartDto dto, int userId);


    }
}
