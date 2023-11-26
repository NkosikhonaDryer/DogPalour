using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogPalour.Models
{
    public class ServiceDogVM
    {
        public int servicerId { get; set; }
        public int DogServicerId { get; set; }
        
        public string picPath { get; set; }
        public int DogId { get; set; }
        public string AssgnState { get; set; }
        public string EmailAddres { get; set; }
        public string ServiceType { get; set; }
        public string EmapName { get; set; }
        public string SurnameName { get; set; }
    }
}