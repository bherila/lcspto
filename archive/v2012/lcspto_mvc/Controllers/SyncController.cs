using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using lcspto_mvc.Models;

namespace lcspto_mvc.Controllers
{
    public class SyncController : Controller
    {
        public ActionResult Index() {
            return new EmptyResult();
        }

        public ActionResult FileList() {
            var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var sm = new SyncModel();
            string root = Request["vpath"];
            string physicalRoot = Server.MapPath(root);
            List<string> dirs = new List<string> { physicalRoot };
            while (dirs.Count > 0) {
                string d = dirs[0];
                dirs.RemoveAt(0);

                foreach (var f in System.IO.Directory.GetFiles(d)) {
                    var fi = new System.IO.FileInfo(f);
                    using (var fiStream = fi.OpenRead()) {
                        sm.FileItems.Add(new FileItemModel() {
                            filelen = (int) fi.Length,
                            filemd5 = Convert.ToBase64String(sha1.ComputeHash(fiStream)),
                            filename = f.ToLower().Replace(physicalRoot.ToLower(), "")
                        });
                        fiStream.Dispose();
                    }
                }

                dirs.AddRange(System.IO.Directory.GetDirectories(d));
            }
            return View(sm);
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