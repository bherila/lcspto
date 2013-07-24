using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lcspto_mvc;

namespace ElixCms.Controllers
{
    public class AdminFormsController : Controller
    {
        private DataModel db = new DataModel();

        // GET: /AdminForms/
        public ViewResult Index() {
            return View(db.forms.ToList());
        }

        // GET: /AdminForms/Details/5
        public ViewResult Details(int id) {
            form form = db.forms.Single(f => f.ID == id);
            return View(form);
        }

        // GET: /AdminForms/Create
        public ActionResult Create() {
            return View();
        }

        // POST: /AdminForms/Create
        [HttpPost]
        public ActionResult Create(form form) {
            if (ModelState.IsValid) {
                db.forms.AddObject(form);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(form);
        }

        // GET: /AdminForms/Edit/5
        public ActionResult Edit(int id) {
            form form = db.forms.Single(f => f.ID == id);
            return View(form);
        }

        // POST: /AdminForms/Edit/5
        [HttpPost]
        public ActionResult Edit(form form) {
            if (ModelState.IsValid) {
                db.forms.Attach(form);
                db.ObjectStateManager.ChangeObjectState(form, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        // GET: /AdminForms/Delete/5
        public ActionResult Delete(int id) {
            form form = db.forms.Single(f => f.ID == id);
            return View(form);
        }

        // POST: /AdminForms/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            form form = db.forms.Single(f => f.ID == id);
            db.forms.DeleteObject(form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}