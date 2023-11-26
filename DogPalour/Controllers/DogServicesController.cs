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
    public class DogServicesController : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: DogServices
        public ActionResult Index()
        {
            return View(db.DogServices.ToList());
        }

        // GET: DogServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogService dogService = db.DogServices.Find(id);
            if (dogService == null)
            {
                return HttpNotFound();
            }
            return View(dogService);
        }

        // GET: DogServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DogId,ServiceID,ServiceType,Description,Price")] DogService dogService,int DogId,int ServiceID,string ServiceType,string Description,double Price)
        {
            if (ModelState.IsValid)
            {
               var service = db.DogServices.Where(a => a.DogId == DogId).ToList();
               
                if(service.Where(a => a.ServiceID == ServiceID).Count() == 0)
                {
                    dogService.DogId = DogId;
                    dogService.ServiceID = ServiceID;
                    dogService.ServiceType = ServiceType;
                    dogService.Description = Description;
                    dogService.Price = Price;
                    db.DogServices.Add(dogService);
                    db.SaveChanges();
                    Dog1 dog1 = db.Dog1s.Find(DogId);
                    Booking booking = db.Bookings.Find(dog1.BookingID);
                    booking.SubTotal += dogService.Price;
                    db.Entry(booking).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ServiceAdding"] = "Service added successfully";
                    return RedirectToAction("Details", "Dog1", new { id = DogId });
                }
                else
                {
                    TempData["ServiceAdding"] = "Service already added";

                    return RedirectToAction("Details", "Dog1", new { id = DogId });
                }
                
            }

            return View(dogService);
        }

        // GET: DogServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogService dogService = db.DogServices.Find(id);
            if (dogService == null)
            {
                return HttpNotFound();
            }
            return View(dogService);
        }

        // POST: DogServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DogId,ServiceID,ServiceType,Description,Price")] DogService dogService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dogService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dogService);
        }

        // GET: DogServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogService dogService = db.DogServices.Find(id);
            if (dogService == null)
            {
                return HttpNotFound();
            }
            return View(dogService);
        }

        // POST: DogServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DogService dogService = db.DogServices.Find(id);
            db.DogServices.Remove(dogService);
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
