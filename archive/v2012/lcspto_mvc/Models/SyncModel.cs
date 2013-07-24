using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lcspto_mvc.Models
{
    public class SyncModel
    {
        public List<FileItemModel> FileItems = new List<FileItemModel>();
        public List<string> Subdirectories = new List<string>();
    }

    public class FileItemModel
    {
        public string filename;
        public int filelen;
        public string filemd5;
    }
}