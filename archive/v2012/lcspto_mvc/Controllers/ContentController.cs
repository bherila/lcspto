using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using lcspto_mvc;
using System.Web.Security;
using lcspto_mvc.Models;

namespace lcspto_mvc.Controllers
{

    public class ContentController : Controller
    {

        //
        // GET: /Content/
        public ActionResult Index(string id) {
            using (DataModel db = new DataModel()) {
                if (String.IsNullOrEmpty(id))
                    id = "default";

                // get the content item
                content currentPage;
                {
                    var f = from x in db.contents
                            where x.PageName == id
                            select x;

                    currentPage = f.FirstOrDefault();
                    db.Detach(currentPage);
                    if (currentPage == null) {
                        Response.StatusCode = 404;
                        Response.End();
                        return new EmptyResult();
                    }
                }

                // build right nav
                {
                    // get children (if any)
                    var childrenQuery = (from x in db.contents
                                            where x.ParentPageName == currentPage.PageName
                                            select x).ToArray();

                    // get the page name and title of each child
                    var childrenData = Array.ConvertAll(childrenQuery, c => new string[] {c.PageName, c.HtmlTitle});

                    if (childrenData.Length > 0) {
                        // use children
                        ViewBag.RightNavTitle = currentPage.HtmlTitle;
                        ViewBag.RightNavItems = childrenData;
                    }
                    else
                    {
                        
                        // no children; use siblings instead
                        var siblings = (from x in db.contents
                                        where x.ParentPageName == currentPage.ParentPageName
                                        select x).ToArray();
                        var cs = Array.ConvertAll(siblings, s => new string[] { s.PageName, s.HtmlTitle });

                        ViewBag.RightNavTitle = (from x in db.contents
                                                    where x.PageName == currentPage.ParentPageName
                                                    select x.HtmlTitle).DefaultIfEmpty(currentPage.HtmlTitle).FirstOrDefault(); // parent title
                        ViewBag.RightNavItems = cs;
                    }
                }

                // require login
                if (currentPage.LoginRequired && !User.Identity.IsAuthenticated)
                    HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(HttpContext.Request.Url.AbsolutePath));

                return View(currentPage);
            }
        }


    }
}