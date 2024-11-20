using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class SubGroup2
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(SubGroup))]
        public int SG_Id { get; set; }

        [ForeignKey(nameof(MainGroup))]
        public int MG_Id { get; set; }
        public SubGroup SubGroup { get; set; }
        public MainGroup MainGroup { get; set; }
        public ICollection<Items> Items { get; set; } = new HashSet<Items>();

    }
}
