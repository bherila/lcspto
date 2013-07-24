using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lcspto_mvc;

namespace ElixCms.Areas.Admin.Controllers
{ 
    public class AdminContentController : Controller
    {
        private DataModel db = new DataModel();

        //
        // GET: /Admin/AdminContent/

        public ViewResult Index()
        {
            return View(db.contents.ToList());
        }

        //
        // GET: /Admin/AdminContent/Details/5

        public ViewResult Details(long id)
        {
            content content = db.contents.Single(c => c.ID == id);
            return View(content);
        }

        //
        // GET: /Admin/AdminContent/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/AdminContent/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(content content)
        {
            if (ModelState.IsValid)
            {
                db.contents.AddObject(content);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(content);
        }
        
        //
        // GET: /Admin/AdminContent/Edit/5
 
        public ActionResult Edit(long id)
        {
            content content = db.contents.Single(c => c.ID == id);
            return View(content);
        }

        //
        // POST: /Admin/AdminContent/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(content content)
        {
            if (ModelState.IsValid)
            {
                db.contents.Attach(content);
                db.ObjectStateManager.ChangeObjectState(content, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(content);
        }

        //
        // GET: /Admin/AdminContent/Delete/5
 
        public ActionResult Delete(long id)
        {
            content content = db.contents.Single(c => c.ID == id);
            return View(content);
        }

        //
        // POST: /Admin/AdminContent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            content content = db.contents.Single(c => c.ID == id);
            db.contents.DeleteObject(content);
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