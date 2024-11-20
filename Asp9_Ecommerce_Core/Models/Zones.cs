using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class Zones
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }

        // Foreign key for Governments
        [ForeignKey(nameof(Governments))]
        public int Gov_Code { get; set; }

        // Foreign key for Cities
        [ForeignKey(nameof(Cities))]
        public int City_Code { get; set; }

        // Navigation properties
        public Cities Cities { get; set; }
        public Governments Governments { get; set; }
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        public ICollection<Stores> Stores  { get; set; } = new HashSet<Stores>();

    }
}
