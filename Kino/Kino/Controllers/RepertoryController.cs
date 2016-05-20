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
using Kino.ViewModels;

namespace Kino.Controllers
{
    public class RepertoryController : Controller
    {
        private CinemaContext db = new CinemaContext();

        // GET: Repertory
        [Authorize]
        public ActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public ActionResult getAllSeans()
        {

            int cinemaID = (int)Session["Cinema"];

            var seanse = from seans in db.Seans
                         join film in db.Film on seans.FilmId equals film.Id
                         join sala in db.Sala on seans.SalaId equals sala.Id
                         where sala.KinoId == cinemaID
                         select new SeansViewModel()
                         {
                             Id = film.Id,
                             start = seans.Data + " "+seans.Godzina,
                             end = seans.Data + " " + seans.GodzinaZakonczenia,
                             title = film.Tytuł
                         };

            //var seanse = 0;
            return Json(seanse, JsonRequestBehavior.AllowGet);
            //return PartialView();
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
            return PartialView();
        }

        // POST: Repertory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Godzina,Data,Opis,FilmId,SalaId,GodzinaZakonczenia")] Seans seans)
        {
            if (ModelState.IsValid)
            {
                db.Seans.Add(seans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seans);
        }

        [Authorize]
        [HttpGet]
        public ActionResult getAllMovies()
        {
            // ModelState.Clear();
            //Session["page"] = "movie";
            return PartialView("tableFilm",db.Film.ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult getAllRooms()
        {
            // ModelState.Clear();
            //Session["page"] = "movie";
            // List<string> List = new List<string>();
            //string result = myList.Single(s => s == search);
            int cinemaID = (int)Session["Cinema"];

            var sala = from s in db.Sala
                       where s.KinoId == cinemaID
                       select s;

            
            return PartialView("tableRoom", sala.ToList());
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
        [HttpGet]
        public ActionResult Delete()
        {
            int cinemaID = (int)Session["Cinema"];

            var seanse = from seans in db.Seans
                         join film in db.Film on seans.FilmId equals film.Id
                         join sala in db.Sala on seans.SalaId equals sala.Id
                         where sala.KinoId == cinemaID
                         select new RepertoryViewModel()
                         {
                             Id = seans.Id,
                             Data = seans.Data,
                             Godzina = seans.Godzina,
                             GodzinaZakonczenia = seans.GodzinaZakonczenia,
                             Opis = seans.Opis,
                             Film = film.Tytuł
                         };

            
            return PartialView(seanse);
        }

        [HttpGet]
        public ActionResult deleteRepertoryById(int idSeans)
        {
            // int cinemaID = (int)Session["Cinema"];

            Seans senas = db.Seans.Find(idSeans);
            db.Seans.Remove(senas);
            db.SaveChanges();


            return Json("1", JsonRequestBehavior.AllowGet);
        }
        

        // POST: Repertory/Delete/5
        /* [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
         {
             Seans seans = db.Seans.Find(id);
             db.Seans.Remove(seans);
             db.SaveChanges();
             return RedirectToAction("Index");
         }*/


        [HttpGet]
        public void DeleteConfirmed(int id)
        {
            RezerwacjaPrzyjeta rezerwacjaprzyjeta = db.RezerwacjaPrzyjeta.Find(id);
            db.RezerwacjaPrzyjeta.Remove(rezerwacjaprzyjeta);
            db.SaveChanges();
            RezerwacjaZlozona rezerwacjazlozona = db.RezerwacjaZlozona.Find(id);
            db.RezerwacjaZlozona.Remove(rezerwacjazlozona);
            db.SaveChanges();
           // return PartialView("Index", db.Film.ToList());
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
