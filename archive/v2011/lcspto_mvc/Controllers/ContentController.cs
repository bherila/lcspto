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
                content o;
                {
                    var f = from x in db.contents
                            where x.PageName == id
                            select x;

                    o = f.FirstOrDefault();
                    db.Detach(o);
                    if (o == null) {
                        Response.StatusCode = 404;
                        Response.End();
                        return new EmptyResult();
                    }
                }

                // build right nav
                {
                    // get children (if any)
                    var children = (from x in db.contents
                                    where x.ParentPageName == o.PageName
                                    select x).ToArray();
                    var cc = Array.ConvertAll(children, c => new string[] {c.PageName, c.HtmlTitle});

                    if (cc.Length == 0) {
                        // no children; use siblings instead
                        var siblings = (from x in db.contents
                                        where x.ParentPageName == o.ParentPageName
                                        select x).ToArray();
                        var cs = Array.ConvertAll(siblings, s => new string[] { s.PageName, s.HtmlTitle });

                        ViewBag.RightNavTitle = (from x in db.contents
                                                 where x.PageName == o.ParentPageName
                                                 select x.HtmlTitle).DefaultIfEmpty(o.HtmlTitle).FirstOrDefault(); // parent title
                        ViewBag.RightNavItems = cs;
                    }
                    else
                    {
                        ViewBag.RightNavTitle = o.HtmlTitle;
                        ViewBag.RightNavItems = cc;
                    }
                }

                // require login
                if (o.LoginRequired && !User.Identity.IsAuthenticated)
                    HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(HttpContext.Request.Url.AbsolutePath));

                return View(o);
            }
        }


    }
}