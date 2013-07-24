using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lcspto_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            using (var data = new lcspto_mvc.DataModel()) {

                ViewBag.Message = "Welcome to ASP.NET MVC!";
                var upcomingEvents = (from item in data.calendaritems
                                      where item.Date >= DateTime.Today.Date
                                      orderby item.Date ascending
                                      select item).ToList().Take(6).ToList();

                upcomingEvents.ForEach(a => data.Detach(a));
                ViewBag.UpcomingEvents = upcomingEvents;
                ViewBag.FeaturedNews = NewsController.GetFeaturedArticle(data);
            }
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
