using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.CustomersDTO
{
    public class InvoiceItemDto
    {
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string UnitName { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalPrice { get; set; }
    }
}
