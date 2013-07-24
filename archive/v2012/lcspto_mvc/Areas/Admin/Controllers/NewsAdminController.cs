using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lcspto_mvc;

namespace lcspto_mvc.Areas.Admin.Controllers
{ 
    public class NewsAdminController : Controller
    {
        private DataModel db = new DataModel();

        //
        // GET: /Admin/NewsAdmin/

        public ViewResult Index()
        {
            return View(db.news.ToList());
        }

        //
        // GET: /Admin/NewsAdmin/Details/5

        public ViewResult Details(long id)
        {
            news news = db.news.Single(n => n.ID == id);
            return View(news);
        }

        //
        // GET: /Admin/NewsAdmin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/NewsAdmin/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(news news)
        {
            if (ModelState.IsValid)
            {
                db.news.AddObject(news);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(news);
        }
        
        //
        // GET: /Admin/NewsAdmin/Edit/5
 
        public ActionResult Edit(long id)
        {
            news news = db.news.Single(n => n.ID == id);
            return View(news);
        }

        //
        // POST: /Admin/NewsAdmin/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(news news)
        {
            if (ModelState.IsValid)
            {
                db.news.Attach(news);
                db.ObjectStateManager.ChangeObjectState(news, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        //
        // GET: /Admin/NewsAdmin/Delete/5
 
        public ActionResult Delete(long id)
        {
            news news = db.news.Single(n => n.ID == id);
            return View(news);
        }

        //
        // POST: /Admin/NewsAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            news news = db.news.Single(n => n.ID == id);
            db.news.DeleteObject(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}