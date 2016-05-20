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
using System.Net.Mail;
using System.Configuration;

namespace Kino.Controllers
{
    public class ReservationController : Controller
    {
        private CinemaContext db = new CinemaContext();

        // GET: Reservation
        public ActionResult Index()
        {
            return PartialView(db.Seans.ToList());
        }

        // GET: Reservation/Details/5
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

        // GET: Reservation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //http://localhost:8083/Reservation/makeReservation?seansID=16&miejsceID=1&opis=asd&pawelkosa18@gmail.com
        [HttpGet]
        public ActionResult makeReservation(int seanseID,int miejsceID,string opis,string e_mail)
        {
            if (opis != String.Empty) {

                Seans seans = db.Seans.Find(seanseID);
                Sala sala = db.Sala.Find(seans.SalaId);


                Miejsce miejesce = db.Miejsce.Find(miejsceID);

                RezerwacjaZlozona rezerwacja = new RezerwacjaZlozona();
                rezerwacja.Miejsce = miejesce;
                rezerwacja.Opis = opis;
                rezerwacja.SeansId = (int)seanseID;
                
                db.RezerwacjaZlozona.Add(rezerwacja);
                db.SaveChanges();

               // Seans seans = db.Seans.Find(seanseID);
                Film film = db.Film.Find(seans.FilmId);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
                    mail.To.Add(e_mail);
                    mail.Subject = "Rezerwacja została przyjeta";
                   mail.Body = "Twoja rezerwacja na film <strong>"+film.Tytuł + " </strong>na godz. <strong>"+seans.Godzina +"</strong>, miejsce: <strong>"+miejesce.Numer+"</strong>, rzad: <strong>"+miejesce.Rząd+"</strong> została przyjęta";
                   // mail.Body = "Rezerwacja została przyjęta";
                     mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //  mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //  mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["mailAccount"], ConfigurationManager.AppSettings["mailPassword"]);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                RezerwacjaPrzyjeta rezerwacjaPrzyjeta = new RezerwacjaPrzyjeta();

                rezerwacjaPrzyjeta.Miejsce = miejesce;
                rezerwacjaPrzyjeta.Opis = opis;
                rezerwacjaPrzyjeta.SeansId = (int)seanseID;

                db.RezerwacjaPrzyjeta.Add(rezerwacjaPrzyjeta);
                db.SaveChanges();
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
            //db.SaveChanges();



            //return View(seans);
        }

        // GET: Reservation/Edit/5
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

        // POST: Reservation/Edit/5
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
