using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.DTOs.AuthDTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public int GovernmentCode { get; set; }

        [Required]
        public int CityCode { get; set; }

        [Required]
        public int ZoneCode { get; set; }
        [Required]
        public int Cus_ClassId { get; set; }
    }
}
