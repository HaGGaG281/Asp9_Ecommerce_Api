using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class MainGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubGroup> SubGroup { get; set; } = new HashSet<SubGroup>();
        public ICollection<SubGroup2> SubGroup2 { get; set; } = new HashSet<SubGroup2>();
        public ICollection<Items> Items { get; set; } = new HashSet<Items>();


    }
}
