using System.Web.Mvc;

namespace lcspto_mvc.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName {
            get {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {

            context.MapRoute(
                "Admin_default1",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "ElixCms.Areas.Admin.*" }
            ).DataTokens["UseNamespaceFallback"] = true;


        }
    }
}
