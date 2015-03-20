using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace taskman
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "",
				defaults: new { controller = "Frontend", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "LoginForm",
				url: "login",
				defaults: new { controller = "Frontend", action = "Login", id = UrlParameter.Optional }
			);

		}
	}
}