using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class Stores
    {
        public int Id { get; set; }
        public string name { get; set; }

        [ForeignKey(nameof(Governments))]
        public int Government_Code { get; set; }
        public Governments Governments { get; set; }

        [ForeignKey(nameof(Cities))]
        public int City_Code { get; set; }
        public Cities Cities { get; set; }

        [ForeignKey(nameof(Zones))]
        public int Zone_Code { get; set; }
        public Zones Zones { get; set; }

        //public ICollection<Items> Items { get; set; } = new HashSet<Items>();

        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; } = new HashSet<ShoppingCartItem>();

        public ICollection<ItemsStores> ItemsStores { get; set; } = new HashSet<ItemsStores>();



    }   
}
