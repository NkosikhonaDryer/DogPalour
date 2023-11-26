using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogPalour.Models;

namespace DogPalour.Controllers
{

    public class HomeController : Controller
    {
        private DogPalourContext db = new DogPalourContext();
        public ActionResult Index()
        {
            var Services = db.Services.ToList();
            foreach(var item in Services)
            {
                if(item.picPath == null)
                {
                    item.picPath = "~/DefaultImgaes/default-image.jpg";
                    
                }
            }
            ViewBag.Services = Services;
            return View();
        }
        // login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            var Admin = db.Admins.Where(a => a.EmailAdddress == Email).ToList();
            var Customer = db.Customers.Where(a => a.EmailAddress == Email).ToList();
            var Employee = db.Employees.Where(a => a.EmailAddres == Email).ToList();
            string ErrorMsg = "";
            if (Admin.Count() > 0 && Customer.Count() < Admin.Count() && Employee.Count() < Admin.Count())
            {
                Admin admins = db.Admins.Find(Email);
                if (Password == admins.Password)
                {
                    // login
                    return RedirectToAction("AdminHome", "Admins");
                }
                else
                {
                    ErrorMsg = "Invalid login credentials";
                }
            }
            else if (Customer.Count() > 0 && Customer.Count() > Admin.Count() && Employee.Count() < Customer.Count())
            {
                //customer login
                Customer customers = db.Customers.Find(Email);
                if (Password == customers.ConfirmPasswrod)
                {
                    Session["EmailC"] = customers.EmailAddress;
                    return RedirectToAction("CustomerHome", "Customers");
                }
                else
                {
                    ErrorMsg = "Invalid login credentials";
                }
            }
            else if(Employee.Count() > 0 && Employee.Count() > Admin.Count() && Employee.Count() > Customer.Count())
            {
                //Employee login
                Employee employees = db.Employees.Find(Email);
                if(Password == employees.Password)
                {
                    Session["EmpEmail"] = employees.EmailAddres;
                    return RedirectToAction("EmployeeHome", "Employees");
                }
                else
                {
                    ErrorMsg = "Invalid login credentials";
                }
                
            }
            else if (Employee.Count() ==0 && Admin.Count()== 0 && Employee.Count() ==0)
            {
                ErrorMsg = "You do not have an account please get registered";
            }
            ViewBag.ErroMsg = ErrorMsg;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}