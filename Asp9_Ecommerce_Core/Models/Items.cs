using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }

        [ForeignKey(nameof(MainGroup))]
        public int? MG_Id { get; set; }
        
        
        [ForeignKey(nameof(SubGroup))]
        public int SG_Id { get; set; }


        [ForeignKey(nameof(SubGroup2))]
        public int? SG2_Id { get; set; }
        public SubGroup SubGroup { get; set; }
        public MainGroup MainGroup { get; set; }
        public SubGroup2 SubGroup2 { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; } = new HashSet<InvoiceDetail>();
        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; } = new HashSet<ShoppingCartItem>();
        public ICollection<ItemsUnits> ItemsUnits { get; set; } = new HashSet<ItemsUnits>();
        public ICollection<ItemsStores> ItemsStores { get; set; } = new HashSet<ItemsStores>();



    }
}
