using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace taskman
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "endpoint/tasks",
				defaults: new { controller = "Task" }
			);

			config.Routes.MapHttpRoute(
				name: "Complete",
				routeTemplate: "endpoint/tasks/{id}/complete",
				defaults: new { controller = "Task", action = "Complete" }
			);

			config.Routes.MapHttpRoute(
				name: "Patch",
				routeTemplate: "endpoint/tasks/{id}",
				defaults: new { controller = "Task", action = "Patch" }
			);

		}
	}
}
