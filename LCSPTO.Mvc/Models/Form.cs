using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Details;
using Dinamico.Models;

namespace LCSPTO.Mvc
{
    public enum FormSpan { span1, span2, span3 }

    public enum FormBootstrapType { Standard = 0, Search = 1, Inline = 2, Horizontal = 3 }

    [PartDefinition]
    public class Form : PartModelBase
    {
        private static readonly string[] FormCSSTypes = new string[] { "", "form-search", "form-inline", "form-horizontal" };

        [EditableEnum(typeof(FormBootstrapType), SortOrder = 50, Title = "Form Display Type")]
        public virtual FormBootstrapType DisplayType { 
            get { return GetDetail("FormDisplayType", FormBootstrapType.Standard); }
            set { SetDetail("FormDisplayType", value); }
        }

        [EditableCheckBox("Inset", 100)]
        public virtual bool CSS_Well { 
            get { return GetDetail("CSS_Well", false); }
            set { SetDetail("CSS_Well", value); }
        }

        public string CssClass
        {
            get
            {
                return
                    (CSS_Well ? "well " : "") +
                    (FormCSSTypes[(int)DisplayType]);
            }
        }
    }

    [PartDefinition]
    [WithEditableTitle]
    [WithEditableTemplateSelection(ContainerName = Dinamico.Defaults.Containers.Metadata)]
    public class FormField: PartModelBase
    {
        
    }


}