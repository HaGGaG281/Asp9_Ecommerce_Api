using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.CustomersDTO
{
    public class InvoiceReceiptDto
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public double TotalPrice { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }
}
