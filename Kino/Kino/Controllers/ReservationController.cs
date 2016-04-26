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
    public class ReservationController : Controller
    {
        private CinemaContext db = new CinemaContext();

        // GET: Reservation
        public ActionResult Index()
        {
            int cinemaID = (int)Session["Cinema"];
      
           var reservationList = from miejsce in db.Miejsce
                                  join rezerwacja in db.RezerwacjaPrzyjeta on miejsce.Id equals rezerwacja.Miejsce.Id
                                  join sala in db.Sala on miejsce.SalaId equals sala.Id
                                  where sala.KinoId== cinemaID
                                 select new ReservationViewModel()
                                 {
                                    IdRezerwacji = rezerwacja.Id,
                                    NumerMiejsca = miejsce.Numer,
                                    Rząd = miejsce.Rząd,
                                    NazwaSali = sala.Nazwa,
                                    Rzędy = sala.Rzędy,
                                    IloscMiejsc = sala.IloscMiejsc,
                                    SalaId = sala.Id,
                                    OpisRezerwacji = rezerwacja.Opis

                                 };
            return PartialView(reservationList.ToList());
        }

        // GET: Reservation/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int cinemaID = (int)Session["Cinema"];

            var detailReservation = from seans in db.Seans
                                    join rezerwacja in db.RezerwacjaPrzyjeta on seans.Id equals rezerwacja.SeansId
                                    join film in db.Film on seans.FilmId equals film.Id
                                    join sala in db.Sala on seans.SalaId equals sala.Id
                                    where sala.KinoId == cinemaID
                                    where rezerwacja.Id == id
                                    select new DetailsReservationViewModel()
                                    {
                                        IdRezerwacji = rezerwacja.Id,
                                        Godzina = seans.Godzina,
                                        Data = seans.Data,
                                        //Opis rezerwacji
                                        Opis = rezerwacja.Opis,
                                        TytulFilmu = film.Tytuł
                                    };
            //  Seans seans = db.Seans.Find(id);

            return PartialView(detailReservation);         
        }

        // GET: Reservation/Create
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    /*    [HttpPost]
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
        }*/

        // GET: Reservation/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int cinemaID = (int)Session["Cinema"];

            var editReservation = from miejsce in db.Miejsce
                                    join rezerwacja in db.RezerwacjaPrzyjeta on  miejsce.Id equals rezerwacja.Miejsce.Id
                                   join sala in db.Sala on miejsce.SalaId equals sala.Id
                                    where sala.KinoId == cinemaID
                                    where rezerwacja.Id == id
                                    select new ReservationEditViewModel()
                                    {
                                        IdSali = miejsce.SalaId,
                                        IdRezerwacji = rezerwacja.Id,
                                        Numer= miejsce.Numer,
                                        Rząd = miejsce.Rząd
                                    };
          //  Seans seans = db.Seans.Find(id);
          
            return PartialView(editReservation.ToList());
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Edit(IEnumerable<ReservationEditViewModel> reservationEditModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var reservationEdit in reservationEditModel)
                {
                    var idMiejsca = from miejsce in db.Miejsce
                                    join sala in db.Sala on miejsce.SalaId equals sala.Id
                                    where miejsce.Numer == reservationEdit.Numer
                                    where miejsce.Rząd == reservationEdit.Rząd
                                    where miejsce.SalaId == reservationEdit.IdSali
                                    select miejsce;
                    int id = 0;
                    foreach (var key in idMiejsca)
                    {
                        id = key.Id;
                    }

                    var reservation = from rezerwacja in db.RezerwacjaPrzyjeta
                                      where rezerwacja.Id == reservationEdit.IdRezerwacji
                                      select rezerwacja;                    
                    Miejsce miej = db.Miejsce.FirstOrDefault(i => i.Id == id);

                    foreach (var rez in reservation)
                    {
                        rez.Miejsce = miej;
                    }
                    db.SaveChanges();
                  
                }
                int cinemaID = (int)Session["Cinema"];

                var reservationList = from miejsce in db.Miejsce
                                      join rezerwacja in db.RezerwacjaPrzyjeta on miejsce.Id equals rezerwacja.Miejsce.Id
                                      join sala in db.Sala on miejsce.SalaId equals sala.Id
                                      where sala.KinoId == cinemaID
                                      select new ReservationViewModel()
                                      {
                                          IdRezerwacji = rezerwacja.Id,
                                          NumerMiejsca = miejsce.Numer,
                                          Rząd = miejsce.Rząd,
                                          NazwaSali = sala.Nazwa,
                                          Rzędy = sala.Rzędy,
                                          IloscMiejsc = sala.IloscMiejsc,
                                          SalaId = sala.Id,
                                          OpisRezerwacji = rezerwacja.Opis
                                      };

                return PartialView("Index", reservationList);
            }
            return RedirectToAction("Index");
        }

        // GET: Reservation/Delete/5
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

        // POST: Reservation/Delete/5
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
