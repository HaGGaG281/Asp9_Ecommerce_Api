using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class SubGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("MainGroup")]
        public int MG_Id { get; set; }
        public MainGroup MainGroup { get; set; }

        public ICollection<SubGroup2> SubGroup2 { get; set; } = new HashSet<SubGroup2>();
        public ICollection<Items> Items { get; set; } = new HashSet<Items>();

    }
}
