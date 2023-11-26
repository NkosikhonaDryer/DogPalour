using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DogPalour.Models;

namespace DogPalour.Controllers
{
    public class CustomersController : Controller
    {
        private DogPalourContext db = new DogPalourContext();
        public ActionResult CustomerHome()
        {

            var Services = db.Services.ToList();
            foreach (var item in Services)
            {
                if (item.picPath == null)
                {
                    item.picPath = "~/DefaultImgaes/default-image.jpg";

                }
            }
            ViewBag.Services = Services;
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            TempData["LoginError"] = "Your session has ended please login again";
            return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            ViewBag.ErrorMsg = "";
            Customer customer = db.Customers.Find(Email);
            if(customer == null)
            {
                ViewBag.ErrorMsg = "Invalid login credentials";
                return View(customer);

            }
            else
            {
                if(customer.ConfirmPasswrod == Password)
                {
                    Session["EmailC"] = customer.EmailAddress;
                    return RedirectToAction("CustomerHome", "Customers");
                }
                else
                {
                    ViewBag.ErrorMsg = "Invalid login credentials";
                    return View(customer);
                }
            }
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                try
                {
                    id = Session["EmailC"].ToString();
                }
                catch(Exception)
                {

                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        public ActionResult DetailsAdmin(string id)
        {
            if (id == null)
            {
               
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Where(a => a.EmailAddress == id).Single();

            var customers = db.Customers.ToList();
            foreach(var item in customers)
            {
                if(item.EmailAddress == id)
                {
                    customer = item;
                }
            }
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNumber,Firstname,LastName,Gender,EmailAddress,PhoneNumber,Address,ConfirmPasswrod")] Customer customer,string NewPassword)
        {
            ViewBag.Error = "";
            ViewBag.AccountMsg = "";
           // if (ModelState.IsValid)
            //{
                if(customer.ConfirmPasswrod == NewPassword)
                {
                    var customer1 = db.Customers.Where(a => a.EmailAddress == customer.EmailAddress).ToList();
                    var admins = db.Admins.Where(a => a.EmailAdddress == customer.EmailAddress).ToList();
                    var Employees = db.Employees.Where(a => a.EmailAddres == customer.EmailAddress).ToList();
                    if(customer1.Count() == 0 && admins.Count()== 0 && Employees.Count() ==0)
                    {
                       
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        TempData["AccountSuccess"] = "Account created successfully";
                        Session["EmailC"] = customer.EmailAddress;
                        return RedirectToAction("CustomerHome", "Customers");
                   
                    }
                    else
                    {
                    ViewBag.Error = "You alredy created an account just login";
                        return View(customer);
                    }
                   
                }
                else
                {
                    ViewBag.Error = "Password do not match";
                    return View(customer);
                }
                
            //}

           // return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNumber,Firstname,LastName,Gender,EmailAddress,PhoneNumber,Address,ConfirmPasswrod")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
