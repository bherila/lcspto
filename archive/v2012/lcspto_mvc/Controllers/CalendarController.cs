using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lcspto_mvc.Controllers
{
    public class CalendarController : Controller
    {

        private DataModel db = new DataModel();

        //
        // GET: /Calendar/

        public ViewResult Index() {
            return View(db.calendaritems.OrderBy(a => a.Date).ToList());
        }


    }
}
