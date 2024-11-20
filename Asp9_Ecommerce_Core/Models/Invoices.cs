using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class Invoices
    {
        public int Id { get; set; }


        [ForeignKey(nameof(Users))]
        public int Cus_Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? Recieved_date { get; set; }
        public double NetPrice { get; set; }
        public int Transaction_Type { get; set; }
        public int Payment_Type { get; set; }
        public bool is_Posted { get; set; }
        public bool is_Closed { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }= new HashSet<InvoiceDetail>();
        public Users Users { get; set; }

    }
}




// cus 1 -- m invoice
// cus 1 -- 1 invoice