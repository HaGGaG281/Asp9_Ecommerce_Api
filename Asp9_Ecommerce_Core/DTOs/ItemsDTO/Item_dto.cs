using Asp9_Ecommerce_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.ItemsDTO
{
    public class Item_dto
    { 
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public List<int> ItemsUnits { get; set; }
        public List<string> Stores { get; set; } // To hold store names
    }
}
