using Kino.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kino.Controllers
{
    public class CinemaController : Controller
    {
        CinemaContext db = new CinemaContext();
        // GET: Cinema

        //http://localhost:55760/Cinema/getCinemaAllCinema
        public ActionResult getAllCinema()
        {
            var cinema = db.Kino.ToList();
          //  string json = JsonConvert.SerializeObject(cinema);
            return Json(cinema, JsonRequestBehavior.AllowGet);
        }

        //http://localhost:55760/Cinema/getCinemaById?CinemaId=1
        [HttpGet]
        public ActionResult getCinemaById(int CinemaId)
        {
            var cinema = db.Kino.Find(CinemaId);
            //  string json = JsonConvert.SerializeObject(cinema);
            return Json(cinema, JsonRequestBehavior.AllowGet);
        }

        
        
        /*[HttpGet]
        public ActionResult deleteCinemaById(int idCinema)
        {
            db.Kino.RemoveRange(db.Kino.Where(x => x.Id == idCinema));
            db.SaveChanges();

            var cinema = db.Kino.ToList();
            return View("Index",cinema);
        }

        [HttpGet]
        public ActionResult addCinemaById(string cinemaName,string cinemaAdress)
        {
            Kino.Models.Kino kino = new Kino.Models.Kino();
            kino.Nazwa = cinemaName;
            kino.Adres = cinemaAdress;
            db.Kino.Add(kino);
            db.SaveChanges();
           // db.Kino.RemoveRange(db.Kino.Where(x => x.Id == idCinema));
          //  db.SaveChanges();

            var cinema = db.Kino.ToList();
            return View("Index", cinema);
        }*/

        
    }
}