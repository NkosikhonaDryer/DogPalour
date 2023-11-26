using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DogPalour.Models
{
    public class Booking
    {
        [Key]
        [Required]
        [DisplayName("Booking ID")]

        public int BookingId { get; set; }



        public string EmailAddress { get; set; }

       
        public string Firstname { get; set; }
      
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Date and time")]
        public DateTime BookingDate { get; set; }
        public double BookingAmount { get; set; }
        public double SubTotal { get; set; }
        public string CustomerIdnumber { get; set; }
        public string state { get; set; }

        [ForeignKey("Admin")]
        public string AdminEmail { get; set; }
        public virtual Admin Admin { get; set; }
        
        public string PaymentStatus { get; set; }






    }
}