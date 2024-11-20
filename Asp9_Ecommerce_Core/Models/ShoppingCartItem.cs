using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class ShoppingCartItem
    {
        [ForeignKey(nameof(Users))]
        public int Cus_Id { get; set; }

        [ForeignKey(nameof(Items))]
        public int ItemCode { get; set; }

        [ForeignKey(nameof(Stores))]
        public int Store_Id { get; set; }
        public double Quantity { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public double Factor { get; set; }
        public int U_Code { get; set; }

        public Users Users { get; set; }
        public Stores Stores { get; set; }
        public Items Items { get; set; }

    }
}
