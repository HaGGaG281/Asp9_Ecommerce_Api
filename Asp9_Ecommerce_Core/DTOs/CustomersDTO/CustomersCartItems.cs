using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.CustomersDTO
{
    public class CustomersCartItems
    {
        public string Itemname { get; set; }
        public double Quantity { get; set; }
        public int U_Code { get; set; } // يمكن أن يكون رمز الوحدة أو أي قيمة أخرى
        public double Factor { get; set; }
        public double price { get; set; }

    }
}
