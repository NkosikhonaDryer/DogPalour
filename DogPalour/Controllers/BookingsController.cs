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
    public class BookingsController : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: Bookings
        public ActionResult Index()
        {
            string Email;
            try
            {
                Email = Session["EmailC"].ToString();
            }
            catch(Exception)
            {
                return RedirectToAction("Login", "Customers");
            }
            ViewBag.EmailC = Email;
            var bookings = db.Bookings.Where(a => a.CustomerIdnumber == Email);
            ViewBag.Acceped = bookings.Where(a => a.state == "Accepted");
            ViewBag.History = bookings.Where(a => a.state == "Canceled");
            return View(bookings.Where(a => a.state == "Pending").ToList());
        }
        [HttpGet]
        public ActionResult AllBookings()
        {
            var bookings = db.Bookings.Where(A => A.state == "Pending").ToList();
            var Accpted = db.Bookings.Where(A => A.state == "Accepted").ToList();

            ViewBag.Accepted = Accpted;
            return View(bookings);
        }

        [HttpGet]
        public ActionResult AdminAccept(int id)
        {
            Booking booking = db.Bookings.Find(id);
            booking.state = "Accepted";
            HandleEmails handleEmails = new HandleEmails();
            handleEmails.AcceptBookingToCustomer(booking.BookingId);
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();
            //TaskAssign(int BookinId)
            TempData["AssignInfo"] = "Please click Assign task from the staff member you wish to assign this task to";
            return RedirectToAction("ADetails", "Dog1", new { Did = booking.BookingId});
        }
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            var dogs = db.Dog1s.Where(a => a.BookingID == id).ToList();
            ViewBag.Dogs = dogs;
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
        public ActionResult DetailsA(int? id)
        { // admin side
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            var dogs = db.Dog1s.Where(a => a.BookingID == id).ToList();
            ViewBag.Dogs = dogs;
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(string IdNumber)
        {
            try
            {
                if(IdNumber == null)
                {
                    IdNumber = Session["EmailC"].ToString();
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Login", "Customers");
            }
            Customer customer = db.Customers.Find(IdNumber);
            ViewBag.FirstName = customer.Firstname;
            ViewBag.LastName = customer.LastName;
            ViewBag.PhoneNumber = customer.PhoneNumber;
            ViewBag.EmailAddress = customer.EmailAddress;
            ViewBag.IdNumber = IdNumber;
            ViewBag.AdminEmail = new SelectList(db.Admins, "EmailAdddress", "Password");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,BookingDate,BookingAmount,CustomerIdnumber,AdminEmail")] Booking booking,string Firstname,string LastName, string PhoneNumber,string EmailAddress)
        {
            if (ModelState.IsValid)
            {
                var bookings = db.Bookings.Where(a => a.BookingDate == booking.BookingDate).ToList();
                if(bookings.Count() == 0)
                {
                    booking.Firstname = Firstname;
                    booking.PhoneNumber = PhoneNumber;
                    booking.EmailAddress = EmailAddress;
                    booking.LastName = LastName;
                    booking.SubTotal = 0.00;
                    booking.BookingAmount = 0.0;
                    booking.state = "Invalid";
                    booking.PaymentStatus = "Pending";
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = booking.BookingId });
                }
                else
                {
                    TempData["DateError"] = "Date and time already reserved by another Customer";
                    return RedirectToAction("Create", "Bookings", new { IdNumber = booking.CustomerIdnumber });
                }
               
            }

            ViewBag.AdminEmail = new SelectList(db.Admins, "EmailAdddress", "Password", booking.AdminEmail);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id, string State)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminEmail = new SelectList(db.Admins, "EmailAdddress", "Password", booking.AdminEmail);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,BookingDate,BookingAmount,CustomerIdnumber,AdminEmail")] Booking booking)
        {
                
                booking.state = "Accepted";
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                HandleEmails handleEmails = new HandleEmails();
                handleEmails.AcceptBookingToCustomer(booking.BookingId);
                return RedirectToAction("AllBookings","Bookings");
                    
        }
        [HttpPost]
        public ActionResult CancelBooking(string id)
        {
            int ID = Convert.ToInt32(id);
            Booking booking = db.Bookings.Find(ID);
            booking.state = "Canceled";
            HandleEmails handleEmails = new HandleEmails();
            handleEmails.CancelBookingToCustomer(booking.BookingId);
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Bookings");
        }
        [HttpPost]
        public ActionResult DeclineBooking(int BookingId, string Reason)
        {
                 
                Booking booking = db.Bookings.Find(BookingId);
                
                booking.state = "Declined";
                db.Entry(booking).State = EntityState.Modified;
                HandleEmails handleEmails = new HandleEmails();
                handleEmails.DeclineBookingToCustomer(BookingId, Reason);
                db.SaveChanges();
               


            return RedirectToAction("AllBookings","Bookings");
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishBooking(int Id)
        {
            double amount = 0.00;
            double Discount = 0.00;
            Booking booking = db.Bookings.Find(Id);
            Booking booking1 = db.Bookings.Find(Id);
            var dog1 = db.Dog1s.Where(a => a.BookingID == booking.BookingId);
            if(dog1.Count() >= 3)
            {
                Discount = (booking.SubTotal / 100) * 10;
            }
            amount = booking.SubTotal - Discount;

            double vat = (amount / 100) * 15;
            booking.BookingAmount = amount + vat;
            booking.state = "Pending";
            booking.EmailAddress = booking1.EmailAddress;
            booking.SubTotal = booking1.SubTotal;
            booking.LastName = booking1.LastName;
            booking.Firstname = booking1.Firstname;
            booking.PhoneNumber = booking1.PhoneNumber;
            booking.CustomerIdnumber = booking1.CustomerIdnumber;
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();
            HandleEmails handleEmails = new HandleEmails();
            handleEmails.BookingEmailToCustomer(booking.BookingId);
            return RedirectToAction("Details", "Bookings", new { id = booking.BookingId });
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
