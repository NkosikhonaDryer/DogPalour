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
    public class PaymentsController : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: Payments
        public ActionResult Index()
        {
            string Email = "";
            try
            {
                Email = Session["EmailC"].ToString();
            }
            catch(Exception)
            {
                return RedirectToAction("Login", "Customers");
            }
            var Payments = db.Payments.Where(a => a.CustomerEmail == Email).ToList();
            return View(Payments);
        }

        public ActionResult AdminIndex()
        {
           
            var Payments = db.Payments.ToList();
            return View(Payments);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? Nid)
        {
            int? id = Nid;
            ViewBag.Name = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            Customer customer = db.Customers.Find(payment.CustomerEmail);
         
            
                ViewBag.Name = customer.Firstname;
            
            
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }
        public ActionResult AdminDetails(int? id)
        {
            ViewBag.Name = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            Customer customer = db.Customers.Find(payment.CustomerEmail);


            ViewBag.Name = customer.Firstname;


            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }
        // GET: Payments/Create
        public ActionResult Create(int? id)
        {

            Booking booking = db.Bookings.Find(id);
            var payment = db.Payments.Where(a => a.BookingId == id).ToList();
            if(payment.Count() > 0)
            {
                TempData["PaymentMsg"] = "Payment already been made";
               
                return RedirectToAction("Details", "Payments", new { Nid = payment.Single().Id });
            }
            ViewBag.EmailC = booking.EmailAddress;
            ViewBag.AmountDue = booking.BookingAmount;
            ViewBag.BookingId = booking.BookingId;
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerEmail,BookingId,AmountDue,CardNumber,CVV,Exp,PaymentDate,Amount")] Payment payment,string CustomerEmail)
        {
           
           
                if (ModelState.IsValid)
                {
                /*if(payment.Amount != payment.AmountDue)
                {
                  TempData["Amount"] = "Amount payed is not equal to the due amount";
                  return RedirectToAction("Create", "Payments",payment.BookingId);
                }*/
               
                payment.CustomerEmail = CustomerEmail;
                    payment.PaymentDate = DateTime.Now;
                
                    db.Payments.Add(payment);

                    db.SaveChanges();
                    Booking booking = db.Bookings.Find(payment.BookingId);
                Booking booking1 = db.Bookings.Find(payment.BookingId);
                booking.SubTotal = booking1.SubTotal;
                booking.EmailAddress = booking1.EmailAddress;
                booking.Firstname = booking1.Firstname;
                booking.LastName = booking1.LastName;
                booking.PhoneNumber = booking1.PhoneNumber;

                    booking.PaymentStatus = "Payed";
                    db.Entry(booking).State = EntityState.Modified;
                    db.SaveChanges();
                    HandleEmails handleEmails = new HandleEmails();
                handleEmails.PayBookingToCustomer(booking.BookingId, payment.AmountDue);
                    TempData["PaymentSuccess"] = "Payment made succesfully";
                    return RedirectToAction("Details","Payments",new { Nid = payment.Id });
                }
            
           

           

            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerEmail,BookingId,AmountDue,CardNumber,CVV,Exp,PaymentDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
