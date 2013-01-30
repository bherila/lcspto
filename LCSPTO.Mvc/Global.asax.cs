﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace LCSPTO.Mvc
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			// map a simple name to a path
			ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
			{
				Path = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.min.js",
				DebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.js"
			});

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}