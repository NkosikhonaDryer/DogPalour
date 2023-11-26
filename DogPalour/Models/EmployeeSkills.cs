using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogPalour.Models
{
    public class EmployeeSkills
    {
        [Key]
        [Required]
        public int id { get; set; } 
        public string JobOffer { get; set; }
        public int NumYrsExperience { get; set; }
        public string MarketImage { get; set; }
       
        public string EmailAddres { get; set; }
        [ForeignKey("EmailAddres")]
        public virtual Employee Employee { get; set; }

    }
}