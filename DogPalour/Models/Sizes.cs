using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DogPalour.Models
{
    public class Sizes
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int ServiceID { get; set; }
        [Required]
        [DisplayName("Size")]
        public string Sizing { get; set; }
        [Required]
        [DisplayName("Price")]
        public double Priceing { get; set; }

    }
}