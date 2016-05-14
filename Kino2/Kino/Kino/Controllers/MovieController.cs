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
using System.IO;
using System.Text;
using System.Web.UI;

namespace Kino.Controllers
{
    public class MovieController : Controller
    {
        private CinemaContext db = new CinemaContext();

        // GET: Movie
        //[HttpGet]
        [Authorize]      
        public ActionResult Index()
        {
            // ModelState.Clear();
            //Session["page"] = "movie";
            return View(db.Film.ToList());
        }
       


        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        // GET: Movie/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return PartialView(film);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]    
        [Authorize]
        public ActionResult Create(Film film)
        {
            if (ModelState.IsValid)
            {
                db.Film.Add(film);
                db.SaveChanges();
                return View("Index", db.Film.ToList());
            }

            return View("Index", db.Film.ToList());
        }

       

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ModelState.Clear();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return PartialView(film);
        }
        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Tytuł,Rok,Reżyser,Opis")] Film film)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
               // db.Film
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
            //    return PartialView("Index", db.Film.ToList());
            }
            
            return PartialView("Index", db.Film.ToList());
        }

        // GET: Movie/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return PartialView(film);
        }

        // POST: Movie/Delete/5
        [HttpGet]     
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Film.Find(id);
            db.Film.Remove(film);
            db.SaveChanges();
            return PartialView("Index", db.Film.ToList());
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
