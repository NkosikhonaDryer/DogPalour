using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DogPalour.Models
{
    public class DogPalourContext: DbContext 
    {
       public DogPalourContext():base("DogPalourDBEntities")
       {

       }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings  { get; set; }
        public DbSet<Dog1> Dog1s { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Sizes> sizes { get; set; }
        public DbSet<Cilent> Cilents { get; set; }
        public DbSet<DogService> DogServices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSkills> EmployeeSkills { get; set; }

    }
}