using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogPalour.Models
{
    public class DogService
    {
        [Required]
        [Key]
        public int id { get; set; }
       
       
        public int DogId { get; set; }
       
        
        public int ServiceID { get; set; }
        [Required]
        [DisplayName("Service type")]
        public string ServiceType { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Price")]
        public Double Price { get; set; }
        public string AssgnState { get; set; }
        public string EmailAddres { get; set; }
        [ForeignKey("EmailAddres")]
        public virtual Employee Employee { get; set; }
        
        public string CompletionState { get; set; }
    }
}