using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace DogPalour.Models
{
    public class Cilent
    {
        [Key]
        [Required]
        [DisplayName("Email Address")]
        public string ClientEmail{ get; set; }

        [Required]
        [DisplayName("First Name")]
        public string Firstname { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Password")]

        public string ClientPasswrod { get; set; }
    }
}