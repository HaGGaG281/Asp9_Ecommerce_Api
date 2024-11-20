using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Invoices))]
        public int InvoiceId { get; set; }
        
        
        [ForeignKey(nameof(Items))]
        public int ItemCode { get; set; }
        public int Price { get; set; }
        public double Quantity { get; set; }
        public double Factor { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Unit_Code { get; set; }
        public Items Items { get; set; }
        public Invoices Invoices { get; set; }
    }
}
