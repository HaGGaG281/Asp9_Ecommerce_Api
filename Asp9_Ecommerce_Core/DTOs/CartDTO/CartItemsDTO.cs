using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.CartDTO
{
    public class CartItemsDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string ItemUnit { get; set; }


    }
}
