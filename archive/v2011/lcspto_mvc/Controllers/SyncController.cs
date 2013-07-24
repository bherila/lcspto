using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace lcspto_mvc.Controllers
{
    public class SyncController : Controller
    {
        public ActionResult Index() {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult FileList(int id) {
            string root = Request["vpath"];
            string physicalRoot = Server.MapPath(root);
            List<string> dirs = new List<string> { physicalRoot };
            while (dirs.Count > 0) {
                string d = dirs[0];
                dirs.RemoveAt(0);

                dirs.AddRange(System.IO.Directory.GetDirectories(d));
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult PutFile(int id) {
            string filename = Request["dest"];
            Request.Files[0].SaveAs(filename);
            Response.Write("OK");
            return View();
        }

        [HttpPost]
        public ActionResult DeleteFile(int id) {
            string filename = Request["vpath"];
            Response.Write(filename);
            return new EmptyResult();
        }


    }
}