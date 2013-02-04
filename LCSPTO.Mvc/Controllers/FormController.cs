using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2.Web.Mvc;
using N2.Web;

namespace LCSPTO.Mvc.Controllers
{
    [Controls(typeof(Form))]
    public class FormController : ContentController<Form>
    {
        public override ActionResult Index()
        {
            return PartialView((string)CurrentItem.TemplateKey, CurrentItem);
        }
    }

    [Controls(typeof(FormField))]
    public class FormFieldsController : ContentController<FormField>
    {
        public override ActionResult Index()
        {
            return PartialView((string)CurrentItem.TemplateKey, CurrentItem);
        }
    }
}