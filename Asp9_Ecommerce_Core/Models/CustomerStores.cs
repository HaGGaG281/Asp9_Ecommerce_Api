using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class CustomerStores
    {
        [ForeignKey(nameof(Users))]
        public int Cus_Id { get; set; }

        [ForeignKey(nameof(Stores))]
        public int StoreId { get; set; }
        public bool isDefault { get; set; }
        public Users Users { get; set; }
        public Stores Stores { get; set; }
    }
}
