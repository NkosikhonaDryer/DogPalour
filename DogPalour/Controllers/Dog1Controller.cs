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
    public class Dog1Controller : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: Dog1
        public ActionResult Index()
        {
            return View(db.Dog1s.ToList());
        }

        // GET: Dog1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog1 dog1 = db.Dog1s.Find(id);
            if (dog1 == null)
            {
                return HttpNotFound();
            }
            var Service = db.Services.ToList();
            foreach(var item in Service)
            {
                if(item.picPath == null)
                {
                    if (item.picPath == null)
                    {
                        item.picPath = "~/DefaultImgaes/default-image.jpg";
                    }
                }
            }
            ViewBag.Services = Service;
            return View(dog1);
        }
         public ActionResult ADetails(int? Did)
        {
            //Admin side
            if (Did == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog1 dog1 = db.Dog1s.Find(Did);
            if (dog1 == null)
            {
                return HttpNotFound();
            }
            var Service = db.Services.ToList();
            foreach(var item in Service)
            {
               
                
                    if (item.picPath == null)
                    {
                        item.picPath = "~/DefaultImgaes/default-image.jpg";
                    }
                
            }
            ViewBag.Services = Service;
            //getting DogService
            var DogServices = db.DogServices.Where(a => a.DogId == dog1.id).ToList();
            var s = (from d in DogServices
                     join a in Service
                     on d.ServiceID equals a.id
                     select new ServiceDogVM
                     {
                         servicerId = d.ServiceID,
                         DogServicerId = d.id,
                         picPath = a.picPath,
                         DogId = d.DogId,
                         AssgnState = d.AssgnState,
                         EmailAddres = d.EmailAddres,
                         ServiceType = d.ServiceType,
                        

                     }).ToList();
            foreach(var item in s)
            {
                if(item.EmailAddres != null)
                {
                    item.EmapName = DogServices.Where(a => a.id == item.DogServicerId).Single().Employee.FirstName;
                    item.SurnameName = DogServices.Where(a => a.id == item.DogServicerId).Single().Employee.Surname;
                }
            }
            foreach(var item in s)
            {
                if(item.EmailAddres == null)
                {
                    item.AssgnState = "Not assigned";
                }

                if (item.picPath == null)
                {
                    item.picPath = "~/DefaultImgaes/default-image.jpg";
                }
            }
            ViewBag.DogServices = s;
            return View(dog1);
        }

        // GET: Dog1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dog1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,DogType,Age,Size,BookingID")] Dog1 dog1,string Name,string DogType,int Age,string Size,int BookingID)
        {
            if (ModelState.IsValid)
            {
                dog1.Name = Name;
                dog1.DogType = DogType;
                dog1.Age = Age;
                dog1.Size = Size;
                dog1.BookingID = BookingID;
                db.Dog1s.Add(dog1);
                
                db.SaveChanges();

                return RedirectToAction("Details","Bookings",new { id = BookingID });
            }

            return View(dog1);
        }

        // GET: Dog1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog1 dog1 = db.Dog1s.Find(id);
            if (dog1 == null)
            {
                return HttpNotFound();
            }
            return View(dog1);
        }

        // POST: Dog1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,DogType,Age,Size,BookingID")] Dog1 dog1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dog1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog1);
        }

        // GET: Dog1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog1 dog1 = db.Dog1s.Find(id);
            if (dog1 == null)
            {
                return HttpNotFound();
            }
            return View(dog1);
        }

        // POST: Dog1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            
            Dog1 dog1 = db.Dog1s.Find(id);
            int bookingId = dog1.BookingID;
            db.Dog1s.Remove(dog1);
            db.SaveChanges();
            return RedirectToAction("Details","Bookings",new { id = bookingId });
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
