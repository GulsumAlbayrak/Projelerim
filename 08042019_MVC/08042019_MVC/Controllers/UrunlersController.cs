using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _08042019_MVC.Models;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace _08042019_MVC.Controllers
{
    [Authorize(Roles ="Admin")] //buradaki işleri sadece admin yapmakta yetkili artık admin girişi yapılmadan ürün eklenemez

    public class UrunlersController : Controller
    {
        private ETICARETEntities db = new ETICARETEntities();

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            return false;
        }
        public void manuAyar()
        {
            if (isAdminUser())
            {
                ViewBag.display = "block";
            }
            else
            {
                ViewBag.display = "None";
            }
        }



        // GET: Urunlers
        public ActionResult Index()
        {
            manuAyar();
            var urunler = db.Urunler.Include(u => u.Kategori);
            return View(urunler.ToList());
        }

        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            manuAyar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            manuAyar();
            ViewBag.RefKatID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,UrunAdi,UrunAciklamasi,UrunFiyati,RefKatID")] Urunler urunler, HttpPostedFileBase UrunResim)
        {
            manuAyar();
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();

                if (UrunResim != null && UrunResim.ContentLength > 0)
                {
                    string dosyaadi = Path.Combine(Server.MapPath("~/images"), urunler.UrunID + ".jpg");
                    UrunResim.SaveAs(dosyaadi);
                }

                return RedirectToAction("Index");
            }        

            ViewBag.RefKatID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi", urunler.RefKatID);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            manuAyar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefKatID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi", urunler.RefKatID);
            return View(urunler);
        }

        // POST: Urunlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunAdi,UrunAciklamasi,UrunFiyati,RefKatID")] Urunler urunler,HttpPostedFileBase UrunResim)
        {
            manuAyar();
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();

                if (UrunResim != null && UrunResim.ContentLength > 0)
                {
                    string dosyaadi = Path.Combine(Server.MapPath("~/images"), urunler.UrunID + ".jpg");
                    UrunResim.SaveAs(dosyaadi);
                }
                return RedirectToAction("Index");
            }
            ViewBag.RefKatID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi", urunler.RefKatID);
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            manuAyar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manuAyar();
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();

            string filePath = Path.Combine(Server.MapPath("~/images"), urunler.UrunID + ".jpg");
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
                fi.Delete();


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
