using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class ItemsStores
    {
        [ForeignKey(nameof(Stores))]
        public int StoreId { get; set; } // Foreign key for Stores
        public Stores Stores { get; set; }

        [ForeignKey(nameof(Items))]
        public int ItemId { get; set; } // Foreign key for Items
        public Items Items { get; set; }

        public double Balance { get; set; } // Example additional column
        public double ReservedQuantity { get; set; }
        public double Factor { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now; // Example column
    }
}
