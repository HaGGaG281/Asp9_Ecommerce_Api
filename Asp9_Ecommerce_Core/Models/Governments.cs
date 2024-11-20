using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class Governments
    {


        public int Id { get; set; } // Primary Key
        public string Name { get; set; }

        // Navigation property for Cities

        //Option 1: (Cascade) is useful if Cities must always be tied to a Government,
        //but Zones can exist independently even if their Government is deleted.
        //Option 2: (Restrict)  gives you full control over deletions,
        //ensuring you handle related rows explicitly in your code.


        public ICollection<Cities> Cities { get; set; } = new HashSet<Cities>();
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        public ICollection<Stores> Stores { get; set; } = new HashSet<Stores>();

    }
}
