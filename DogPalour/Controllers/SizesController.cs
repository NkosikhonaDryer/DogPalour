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
    public class SizesController : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: Sizes
        public ActionResult Index()
        {
            return View(db.sizes.ToList());
        }

        // GET: Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sizes sizes = db.sizes.Find(id);
            if (sizes == null)
            {
                return HttpNotFound();
            }
            return View(sizes);
        }

        // GET: Sizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceID,Sizing,Priceing")] Sizes sizes,int ServiceID,string Sizing,double Priceing)
        {
            if (ModelState.IsValid)
            {
                var sizesById = db.sizes.Where(a => a.ServiceID == ServiceID).ToList();
                var s = sizesById.Where(A => A.Sizing == Sizing);
                if(s.Count() == 0)
                {
                    sizes.ServiceID = ServiceID;
                    sizes.Sizing = Sizing;
                    sizes.Priceing = Priceing;
                    db.sizes.Add(sizes);
                    db.SaveChanges();
                    TempData["SizeConfirm"] = "Dog size price added successfully";
                    return RedirectToAction("Details", "Services", new { id = ServiceID });
                }
                else
                {
                    TempData["SizeConfirm"] = "Dog size price alredy added";
                    return RedirectToAction("Details", "Services", new { id = ServiceID });
                }
               
            }
            return RedirectToAction("Details", "Services", new { id = ServiceID });

        }

        // GET: Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sizes sizes = db.sizes.Find(id);
            if (sizes == null)
            {
                return HttpNotFound();
            }
            return View(sizes);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ServiceID,Sizing,Priceing")] Sizes sizes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sizes);
        }

        // GET: Sizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sizes sizes = db.sizes.Find(id);
            if (sizes == null)
            {
                return HttpNotFound();
            }
            return View(sizes);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sizes sizes = db.sizes.Find(id);
            int ServiceID = sizes.ServiceID;
            db.sizes.Remove(sizes);
            db.SaveChanges();
            return RedirectToAction("Details", "Services", new { id = ServiceID });
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
