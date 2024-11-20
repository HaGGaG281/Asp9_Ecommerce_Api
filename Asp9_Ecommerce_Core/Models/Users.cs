using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class Users : IdentityUser<int>
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [ForeignKey(nameof(Governments))]
        public int Government_Code { get; set; }

        [ForeignKey(nameof(Cities))]
        public int City_Code { get; set; }

        [ForeignKey(nameof(Zones))]
        public int Zone_Code { get; set; }

        [ForeignKey(nameof(Classifications))]
        public int Cus_ClassId { get; set; }


        public ICollection<Invoices> Invoices { get; set; } = new HashSet<Invoices>();
        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; } = new HashSet<ShoppingCartItem>();
        public Governments Governments { get; set; }
        public Cities Cities { get; set; }
        public Zones Zones { get; set; }
        public Classifications Classifications { get; set; }

    }
}
