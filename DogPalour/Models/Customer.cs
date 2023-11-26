using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DogPalour.Models
{
    public class Customer
    {
        
        [Key]
        [Required]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
       
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

        public string ConfirmPasswrod { get; set; }
    }
}