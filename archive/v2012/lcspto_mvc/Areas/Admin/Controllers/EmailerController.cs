using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lcspto_mvc.Areas.Admin.Models;

namespace lcspto_mvc.Areas.Admin.Controllers
{
    public class EmailerController : Controller
    {
        //
        // GET: /Admin/Emailer/

        public ActionResult Index() {
            ViewBag.RecipientCount = 0;
            ViewBag.QueueLength = 0;

            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailerModel postedModel) {
            return Index();
        }

        public ActionResult EditList() {
            var x = new string[2];
            return View(x);
        }

        [HttpPost]
        public ActionResult EditList(string[] post) {
            return View(post);
        }



    }
}
