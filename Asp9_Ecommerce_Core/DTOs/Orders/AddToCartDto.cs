using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.Orders
{
    public class AddToCartDto
    {
        public int ItemCode { get; set; }
        public int StoreId { get; set; }
        public double Quantity { get; set; } 
        public int U_Code { get; set; } // يمكن أن يكون رمز الوحدة أو أي قيمة أخرى

    }
}
