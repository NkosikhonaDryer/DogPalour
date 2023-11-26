using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogPalour.Models
{
    public class Dog1
    {
        [Required]
        public int id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Dog type")]
        public string DogType { get; set; }
        [Required]
        [DisplayName("Age")]
        public int Age { get; set; }
        [Required]
        [DisplayName("Size")]
        
        public string Size { get; set; }
       
      
        public int BookingID { get;set; }

       



    }
}