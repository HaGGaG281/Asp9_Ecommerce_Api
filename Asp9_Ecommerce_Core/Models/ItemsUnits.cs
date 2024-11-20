using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Models
{
    public class ItemsUnits
    {

        [ForeignKey(nameof(Items))]
        public int ItemCode { get; set; }

        [ForeignKey(nameof(Units))]
        public int UnitCode { get; set; }

        public double Factor { get; set; }
        public Items Items { get; set; }
        public Units Units { get; set; }
    }
}
