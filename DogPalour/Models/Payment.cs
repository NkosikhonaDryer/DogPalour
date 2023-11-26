using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DogPalour.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public int BookingId { get; set; }
        public double AmountDue { get; set; }
        [DisplayName("Card number")]
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Exp { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }

    }
}