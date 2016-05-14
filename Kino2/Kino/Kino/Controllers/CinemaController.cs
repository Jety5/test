using Kino.DAL;
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
        [Authorize]
        public ActionResult Index()
        {
            var cinema = db.Kino.ToList();
           // Session["page"] = " ";
            return View(cinema);
        }

        [Authorize]        
        public ActionResult Main(int CinemaId)
        {
            Models.Kino kino = db.Kino.Find(CinemaId);
           Session["Cinema"] = CinemaId;
           //   string page = Session["page"].ToString();
          //  if (page == "movie")
         //   {
                
          //      return View("Movie/Index",db.Film.ToList());

         //   }
         //   else
         //   {
                return View(kino);
           // }
           
        }       
        
        [HttpGet]
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
        }

        
    }
}