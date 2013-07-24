using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lcspto_mvc.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index(string category, string id) {
            if (string.IsNullOrEmpty(category))
                category = "news";

            using (var s = new DataModel()) {

                // start with all articles in category
                var data = s.news.Where(a => a.Category == category);

                // filter either by id# or header text 
                {
                    int idno;
                    if (int.TryParse(id, out idno))
                        data = data.Where(a => a.ID == idno);
                    else if (!String.IsNullOrEmpty(id))
                        data = data.Where(a => a.HeaderText == id);
                }

                // show newest articles first
                data = data.OrderByDescending(a => a.PublishDate);

                var result = data.ToList();
                result.ForEach(a => s.Detach(a));
                return View(result);
            }

        }


        /// <summary>
        /// Gets the featured article to be displayed on the home page.
        /// </summary>
        /// <returns></returns>
        public static news GetFeaturedArticle(DataModel s) {
            var data = from x in s.news
                       where x.Category == "news"
                       orderby x.PublishDate descending
                       select x;

            var result = data.First();
            s.Detach(result);
            return result;
        }


    }
}
