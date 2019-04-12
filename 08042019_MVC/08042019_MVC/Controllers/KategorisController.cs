using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _08042019_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _08042019_MVC.Controllers
{

    public class KategorisController : Controller
    {
        private ETICARETEntities db = new ETICARETEntities();

        public  Boolean isAdminUser()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if(s[0].ToString()=="Admin")
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
            if(isAdminUser())
            {
                ViewBag.display = "block";
            }
            else
            {
                ViewBag.display = "None";
            }
        }


        // GET: Kategoris
        public ActionResult Index()
        {
            manuAyar();
            return View(db.Kategori.ToList());
        }

        // GET: Kategoris/Details/5
        public ActionResult Details(int? id)
        {
            manuAyar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }
        [Authorize(Roles = "Admin")] //buradaki işleri sadece admin yapmakta yetkili artık admin girişi yapılmadan ürün eklenemez
        // GET: Kategoris/Create
        public ActionResult Create()
        {
            manuAyar();
            return View();
        }

        // POST: Kategoris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriID,KategoriAdi")] Kategori kategori)
        {
            manuAyar();
            if (ModelState.IsValid)
            {
                db.Kategori.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategori);
        }
        [Authorize(Roles = "Admin")] //buradaki işleri sadece admin yapmakta yetkili artık admin girişi yapılmadan ürün eklenemez
        // GET: Kategoris/Edit/5
        public ActionResult Edit(int? id)
        {
            manuAyar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategoris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriID,KategoriAdi")] Kategori kategori)
        {
            manuAyar();
            if (ModelState.IsValid)
            {
                db.Entry(kategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }
        [Authorize(Roles = "Admin")] //buradaki işleri sadece admin yapmakta yetkili artık admin girişi yapılmadan ürün eklenemez
        // GET: Kategoris/Delete/5
        public ActionResult Delete(int? id)
        {
            manuAyar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }
        [Authorize(Roles = "Admin")] //buradaki işleri sadece admin yapmakta yetkili artık admin girişi yapılmadan ürün eklenemez
        // POST: Kategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manuAyar();
            Kategori kategori = db.Kategori.Find(id);
            db.Kategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            manuAyar();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
