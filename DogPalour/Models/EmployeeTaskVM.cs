using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogPalour.Models
{
    public class EmployeeTaskVM
    {
        public int BookingId { get; set; }
        public int DogId { get; set; }
        public int DogServiceId { get; set; }
        public string DogName { get; set; }
        public string SeviceNeeded { get; set; }
        public DateTime BookingDate { get; set; }
        public string CompleteState { get; set; }
    }
}