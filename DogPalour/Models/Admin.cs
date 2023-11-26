using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DogPalour.Models
{
    public class Admin
    {
        [Display(Name="Email Address")]
        [Key]
        public string EmailAdddress { get; set; }
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        
    }
}