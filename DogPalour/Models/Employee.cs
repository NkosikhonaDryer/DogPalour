using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogPalour.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Enater Email Address")]
        [DisplayName("Email address")]
        [DataType(DataType.EmailAddress)]
        [Key]
        public string EmailAddres { get; set; }
        [DisplayName("Id number")]
        [MaxLength(13)]
        [MinLength(13)]
        public string IdNumber { get; set; }
        [DisplayName("First name")]
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter surname")]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Select gender")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        
        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Enter  phone number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        [DisplayName("Service offer")]
        [Required(ErrorMessage = "Select the type of services Offered by employee")]
       
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public int Timing { get; set; }
        public virtual ICollection<EmployeeSkills> EmployeeSkills { get; set; }
        public virtual ICollection<DogService> DogServices { get; set; }

    }
}