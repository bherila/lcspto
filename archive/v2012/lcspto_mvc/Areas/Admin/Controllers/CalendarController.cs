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
    public class AdminCalendarController : Controller
    {
        private DataModel db = new DataModel();

        //
        // GET: /Admin/Calendar/

        public ViewResult Index() {
            return View(db.calendaritems.ToList());
        }

        //
        // GET: /Admin/Calendar/Details/5

        public ViewResult Details(int id) {
            calendaritem calendaritem = db.calendaritems.Single(c => c.ID == id);
            return View(calendaritem);
        }

        //
        // GET: /Admin/Calendar/Create

        public ActionResult Create() {
            return View();
        }

        //
        // POST: /Admin/Calendar/Create

        [HttpPost]
        public ActionResult Create(calendaritem calendaritem) {
            if (ModelState.IsValid) {
                db.calendaritems.AddObject(calendaritem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calendaritem);
        }

        //
        // GET: /Admin/Calendar/Edit/5

        public ActionResult Edit(int id) {
            calendaritem calendaritem = db.calendaritems.Single(c => c.ID == id);
            return View(calendaritem);
        }

        //
        // POST: /Admin/Calendar/Edit/5

        [HttpPost]
        public ActionResult Edit(calendaritem calendaritem) {
            if (ModelState.IsValid) {
                db.calendaritems.Attach(calendaritem);
                db.ObjectStateManager.ChangeObjectState(calendaritem, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calendaritem);
        }

        //
        // GET: /Admin/Calendar/Delete/5

        public ActionResult Delete(int id) {
            calendaritem calendaritem = db.calendaritems.Single(c => c.ID == id);
            return View(calendaritem);
        }

        //
        // POST: /Admin/Calendar/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            calendaritem calendaritem = db.calendaritems.Single(c => c.ID == id);
            db.calendaritems.DeleteObject(calendaritem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}