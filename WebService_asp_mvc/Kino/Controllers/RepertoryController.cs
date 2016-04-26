using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kino.DAL;
using Kino.Models;

namespace Kino.Controllers
{
    public class RepertoryController : Controller
    {
        private CinemaContext db = new CinemaContext();

        // GET: Repertory
        public ActionResult Index()
        {
            return PartialView(db.Seans.ToList());
        }

        // http://localhost:55760/Repertory/getAllSeans?CinemaId=1
        [HttpGet]
        public ActionResult getAllSeans(int? CinemaId)
        {
            var seanse = from seans in db.Seans
                         join film in db.Film on seans.FilmId equals film.Id
                         join sala in db.Sala on seans.SalaId equals sala.Id
                         where sala.KinoId == CinemaId
                         select new SeansViewModel()
                                  {
                                      Data = seans.Data,
                                      Godzina = seans.Godzina,
                                      Opis = seans.Opis,
                                      TytułFilmu = film.Tytuł,                                      
                                      OpisFilmu = film.Opis,
                                      RezyserFilmu = film.Reżyser,
                                      RokFilmu = (film.Rok).ToString(),
                                      NazwaSali = sala.Nazwa                                  

                         };



           // var movie = db.Seans.ToList();

            return Json(seanse, JsonRequestBehavior.AllowGet);
        }

        // GET: Repertory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seans seans = db.Seans.Find(id);
            if (seans == null)
            {
                return HttpNotFound();
            }
            return View(seans);
        }

        // GET: Repertory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repertory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Godzina,Data,Opis,FilmId,SalaId")] Seans seans)
        {
            if (ModelState.IsValid)
            {
                db.Seans.Add(seans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seans);
        }

        // GET: Repertory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seans seans = db.Seans.Find(id);
            if (seans == null)
            {
                return HttpNotFound();
            }
            return View(seans);
        }

        // POST: Repertory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Godzina,Data,Opis,FilmId,SalaId")] Seans seans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seans);
        }

        // GET: Repertory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seans seans = db.Seans.Find(id);
            if (seans == null)
            {
                return HttpNotFound();
            }
            return View(seans);
        }

        // POST: Repertory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seans seans = db.Seans.Find(id);
            db.Seans.Remove(seans);
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
