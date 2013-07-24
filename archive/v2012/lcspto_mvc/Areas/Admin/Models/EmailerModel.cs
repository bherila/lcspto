using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lcspto_mvc.Areas.Admin.Models
{
    public class EmailerModel
    {

        [Required]
        public string Subject { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Body { get; set; }

    }
}
