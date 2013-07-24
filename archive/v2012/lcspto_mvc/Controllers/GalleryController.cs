using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace lcspto_mvc.Controllers
{
    public class GalleryController : Controller
    {


        const string GALLERY_ROOT_URL = @"~/elixcms/gallery/";
        string GALLERY_ROOT_DIR = System.Web.Hosting.HostingEnvironment.MapPath(GALLERY_ROOT_URL); //@"D:\PTO.pfo\";
        string PPTYPE_THUMB_DIR = @"thumbs\";
        string PPTYPE_MEDIUM_DIR = @"med\";
        
        
        public GalleryController()
            : base() {
            char ps = System.IO.Path.DirectorySeparatorChar;

            if (GALLERY_ROOT_DIR.Last() != ps)
                throw new ArgumentException(GALLERY_ROOT_DIR.Last().ToString() + " vs " + ps);

            if (PPTYPE_MEDIUM_DIR.Last() != ps || PPTYPE_THUMB_DIR.Last() != ps)
                throw new ArgumentException();

            if (PPTYPE_MEDIUM_DIR[0] == ps || PPTYPE_THUMB_DIR[0] == ps)
                throw new ArgumentException("No leading slash on THUMBDIR or MEDIUMDIR");

            sRoot = GALLERY_ROOT_DIR + PPTYPE_THUMB_DIR;
        }

        //
        // GET: /Photo/

        string sRoot;

        [Authorize]
        public ActionResult Index() {
            PhotoDirectoryModel model = new PhotoDirectoryModel(null);
            model.VirtualPath = Request["directory"] ?? "";
            model.Name = Path.GetFileName(model.VirtualPath);
            model.Photos.AddRange(GetPhotos(Path.Combine(sRoot, Slash2System(model.VirtualPath))));
            FillSubdirectories(model);
            GetPhotoMetrics(model);

            // Link to parent directory
            ViewBag.UpLink = Url.Action("Index", new {
                Directory =
                    model.VirtualPath.Substring(0, model.VirtualPath.LastIndexOf(Path.DirectorySeparatorChar) + 1).TrimEnd('/', '\\')
            });

            return View(model);
        }

        void GetPhotoMetrics(PhotoDirectoryModel model) {
            // Get Thumbnail bounds
            int dir_key = model.VirtualPath.GetHashCode();
            using (DataModel DataContext = new DataModel()) {
                var pmquery = from x in DataContext.photo_propertycache
                              where x.DirHash == dir_key
                              select new { x.FilenameHash, x.Width, x.Height };

                var pmNewItems = new List<int[]>();
                var pmdata = pmquery.ToDictionary(a => a.FilenameHash);
                Parallel.ForEach(
                    model.Photos,
                    new ParallelOptions() { MaxDegreeOfParallelism = 8 },
                    x => {
                        int key = x.Filename.GetHashCode();
                        if (pmdata.ContainsKey(key)) {

                            // already know width & height from last time
                            var obj = pmdata[key];
                            x.ThumbWidth = obj.Width;
                            x.ThumbHeight = obj.Height;

                        }
                        else {
                            try {
                                // have to compute thumb width and height
                                using (Image bmp = Bitmap.FromFile(x.Filename)) {
                                    x.ThumbWidth = bmp.Width;
                                    x.ThumbHeight = bmp.Height;
                                    bmp.Dispose();
                                }

                                // multiple insert!
                                pmNewItems.Add(new int[] { key, dir_key, x.ThumbWidth, x.ThumbHeight });
                            }
                            catch { }
                        }
                    });

                // bypasses Entity Framework :-\
                if (pmNewItems.Count > 0) {
                    var conn = ((System.Data.EntityClient.EntityConnection)DataContext.Connection).StoreConnection;
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    var INSERTS = pmNewItems.ConvertAll(a => "(" + String.Join(",", a) + ")");
                    using (var cmd = conn.CreateCommand()) {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"INSERT IGNORE INTO photo_propertycache (filename_hash, dir_hash, width, height) VALUES "
                            + String.Join(", ", INSERTS);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        void FillSubdirectories(PhotoDirectoryModel root) {
            if (root.Directories == null)
                throw new ArgumentException();

            string[] sPhysical = Directory.GetDirectories(
                System.IO.Path.Combine(sRoot, Slash2System(root.VirtualPath)));
            string[] sVirtual = Array.ConvertAll(sPhysical, a => a.Substring(sRoot.Length));

            for (int i = 0; i < sPhysical.Length; ++i) {
                PhotoDirectoryModel sub = new PhotoDirectoryModel(root);
                sub.VirtualPath = sVirtual[i];
                sub.Name = Path.GetFileName(sVirtual[i]);
                root.Directories.Add(sub);

                //FillSubdirectories(sub);
                //sub.Photos.AddRange(GetPhotos(sPhysical[i]));
            }
        }


        IEnumerable<PhotoModel> GetPhotos(string physicalRoot) {
            if (String.IsNullOrEmpty(physicalRoot))
                throw new ArgumentException();

            string[] files = Directory.GetFiles(physicalRoot, "*.jpg");
            PhotoModel[] photos = new PhotoModel[files.Length];
            for (int i = 0; i < photos.Length; ++i) {
                string vPath = files[i].Substring(GALLERY_ROOT_DIR.Length + PPTYPE_THUMB_DIR.Length);
                photos[i] = new PhotoModel(files[i]);
                photos[i].VirtualPath = vPath;
                photos[i].ThumbUrl = Slash2Virtual(GALLERY_ROOT_URL + PPTYPE_THUMB_DIR + vPath);
                photos[i].MedUrl = Slash2Virtual(GALLERY_ROOT_URL + PPTYPE_MEDIUM_DIR + vPath);
            }
            return photos;
        }

        // Physical path utility -------



        public static string Slash2System(string virtualPath) {
            string p = virtualPath;
            char ps = System.IO.Path.DirectorySeparatorChar;
            if (ps != '/')
                p = p.Replace('/', ps);
            if (ps != '\\')
                p = p.Replace('\\', ps);
            p.Trim(ps);
            return p;
        }

        public static string Slash2Virtual(string path) {
            string p = path;
            char ps = System.IO.Path.DirectorySeparatorChar;
            p = p.Replace('\\', '/');
            p = p.Trim('/');
            return p;
        }

    }
}
