
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lcspto_mvc.Controllers
{
    public sealed class PhotoDirectoryModel
    {
        public PhotoDirectoryModel(PhotoDirectoryModel parent) {
            this.Parent = parent;
            Directories = new List<PhotoDirectoryModel>();
            Photos = new List<PhotoModel>();
        }

        public string VirtualPath { get; set; }
        public string Name { get; set; }

        public List<PhotoDirectoryModel> Directories { get; private set; }
        public List<PhotoModel> Photos { get; private set; }

        public PhotoDirectoryModel Parent { get; set; }
    }

    public sealed class PhotoModel
    {
        public PhotoModel(string filename) {
            this.Filename = filename;
            this.DisplayName = System.IO.Path.GetFileNameWithoutExtension(Filename);
        }

        [Key]
        public string Filename { get; private set;  }

        public string DisplayName { get;  private set; }

        public string VirtualPath { get; set; }

        public string ThumbUrl { get; set; }
        public string MedUrl { get; set; }


        public int ThumbWidth { get; set; }

        public int ThumbHeight { get; set; }
    }

}