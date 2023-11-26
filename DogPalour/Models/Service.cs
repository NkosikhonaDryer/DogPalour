
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogPalour.Models
{
    public class Service
    {
        [Key]
        public int id { get; set; }
        public int ServiceID { get; set; }
        [Required]
        [DisplayName("Service type")]
        public string ServiceType { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
        public string picPath { get; set; }
        
        public string AddState { get; set; }
      


    }
}