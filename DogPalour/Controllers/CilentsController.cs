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
    public class CilentsController : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: Cilents
        public ActionResult Index()
        {
            return View(db.Cilents.ToList());
        }

        // GET: Cilents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cilent cilent = db.Cilents.Find(id);
            if (cilent == null)
            {
                return HttpNotFound();
            }
            return View(cilent);
        }

        // GET: Cilents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cilents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientEmail,Firstname,LastName,Gender,PhoneNumber,Address,ClientPasswrod")] Cilent cilent)
        {
            if (ModelState.IsValid)
            {
                db.Cilents.Add(cilent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cilent);
        }

        // GET: Cilents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cilent cilent = db.Cilents.Find(id);
            if (cilent == null)
            {
                return HttpNotFound();
            }
            return View(cilent);
        }

        // POST: Cilents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientEmail,Firstname,LastName,Gender,PhoneNumber,Address,ClientPasswrod")] Cilent cilent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cilent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cilent);
        }

        // GET: Cilents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cilent cilent = db.Cilents.Find(id);
            if (cilent == null)
            {
                return HttpNotFound();
            }
            return View(cilent);
        }

        // POST: Cilents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cilent cilent = db.Cilents.Find(id);
            db.Cilents.Remove(cilent);
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
