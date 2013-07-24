using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace lcspto_mvc
{
    [XmlType()]
    [MetadataType(typeof(calendaritem_validation))]
    public partial class calendaritem
    {

    }

    public class calendaritem_validation
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }

    [XmlType()]
    [MetadataType(typeof(content_validation))]
    public partial class content { }

    public class content_validation
    {
        [Required]
        public string ParentPageName { get; set; }

        [DataType(DataType.MultilineText)]
        public string HtmlText { get; set; }

        [Required]
        public string PageName { get; set; }

        [Required]
        public string HtmlTitle { get; set; }

        [Required]
        public bool LoginRequired { get; set; }

    }

}