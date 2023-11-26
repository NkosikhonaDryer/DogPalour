using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DogPalour.Models;

namespace DogPalour.Controllers
{
    public class ServicesController : Controller
    {
        private DogPalourContext db = new DogPalourContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.ToList();
            foreach(var item in services)
            {
                if(item.picPath == null)
                {
                    item.picPath = "~/DefaultImgaes/default-image.jpg";
                }
            }
            return View(services);
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if(service.picPath == null)
            {
                service.picPath = "~/DefaultImgaes/default-image.jpg";
            }
            ViewBag.Path = service.picPath;
           
            var sizes = db.sizes.Where(a => a.ServiceID == service.id).ToList();
            ViewBag.Sizes = sizes;
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // Details to confirm the adding of a service
        [HttpGet]
        public ActionResult CornfirmDetails(int? id,int dogId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service.picPath == null)
            {
                service.picPath = "~/DefaultImgaes/default-image.jpg";
            }
            //
            Dog1 dog1 = db.Dog1s.Find(dogId);
            var Sizes = db.sizes.Where(a => a.ServiceID == service.id);
            Sizes Price = new Sizes();
            try
            {
                Price = Sizes.Where(a => a.Sizing == dog1.Size).Single();
                
            }
            catch(Exception)
            {
                TempData["ServiceAdding"] = "Sevice temporally not available";
                return RedirectToAction("Details","Dog1", new {id = dogId });
            }
            if (Price == null)
            {
                ViewBag.Price = "Not set";
            }
            ViewBag.Price = Price.Priceing;
            ViewBag.dogId = dogId;
            
            ViewBag.Path = service.picPath;
            var sizes = db.sizes.Where(a => a.ServiceID == service.id).ToList();
            ViewBag.Sizes = sizes.Where(a => a.Sizing == dog1.Size);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }
        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ServiceID,ServiceType,Description,Price")] Service service, HttpPostedFileBase AlbumCover)
        {
            if (ModelState.IsValid)
            { //******Saving Album Cover image on the folder "Service cover image" and on the database And folder"*** 
                if (AlbumCover != null)
                {
                    string fileName = Path.GetFileName(AlbumCover.FileName);
                    string extection = Path.GetExtension(AlbumCover.FileName);
                    // fileName += extection;
                    string name = fileName;
                   service.picPath = "~/DefaultImgaes/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/DefaultImgaes/"), fileName);
                    AlbumCover.SaveAs(fileName);

                }
                //******Saving Album Cover image on the folder "Service cover image" and on the database And folder***

                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Details",new { id = service.id });
            }

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.PicLink = service.picPath;
            if (service.picPath == null)
            {
                service.picPath = "~/DefaultImgaes/default-image.jpg";
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ServiceID,ServiceType,Description,Price")] Service service, HttpPostedFileBase AlbumCover,string PicLink)
        {
            if (ModelState.IsValid)
            {
                //******Saving Album Cover image on the folder "Service cover image" and on the database And folder"*** 
                if (AlbumCover != null)
                {
                    string fileName = Path.GetFileName(AlbumCover.FileName);
                    string extection = Path.GetExtension(AlbumCover.FileName);
                    // fileName += extection;
                    string name = fileName;
                    service.picPath = "~/DefaultImgaes/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/DefaultImgaes/"), fileName);
                    AlbumCover.SaveAs(fileName);

                }
                else
                {
                    service.picPath = PicLink;
                }
                //******Saving Album Cover image on the folder "Service cover image" and on the database And folder***
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details",new { id = service.id });
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            string Sname = service.ServiceType;
            db.Services.Remove(service);
            db.SaveChanges();
            TempData["ServiceMsg"] = Sname + " removed successfully"; 
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
