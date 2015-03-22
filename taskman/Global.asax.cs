using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using System.Web.Security;

namespace taskman
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

		class UserIdentity : System.Security.Principal.IIdentity
		{
			FormsAuthenticationTicket _ticket;

			public UserIdentity(FormsAuthenticationTicket ticket)
			{
				_ticket = ticket;
			}

			public string AuthenticationType
			{
				get { return "FormAuthentiocation"; }
			}

			public bool IsAuthenticated
			{
				get { return true; }
			}

			public string Name
			{
				get { return _ticket.Name; }
			}
		}

		private void AuthByCookies(FormsAuthenticationEventArgs args)
		{
			try
			{
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
				  Request.Cookies[FormsAuthentication.FormsCookieName].Value);

				args.Context.User = new System.Security.Principal.GenericPrincipal(
				  new UserIdentity(ticket),
				  new string[] { "user" });
			}
			catch (Exception e)
			{
				throw new HttpException("Invalid Cookie");
			}
		}

		public void AuthByForm(FormsAuthenticationEventArgs args)
		{
			string username = this.Request.Params["username"], password = this.Request.Params["password"],
				action = this.Request.Params["action"];

			if (username != null && action != null)
			{
				if (action == "signup")
				{
					Membership.CreateUser(username, password);
				}

				if (Membership.ValidateUser(username, password))
				{

					args.Context.User = new System.Security.Principal.GenericPrincipal(
							  new System.Security.Principal.GenericIdentity(username, "formAuth"),
							  new string[] { "user" });

					FormsAuthentication.SetAuthCookie(username, false);
					//FormsAuthentication.RedirectFromLoginPage(username, true);
				}
			}
		}

		public void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
		{
			if (FormsAuthentication.CookiesSupported)
			{
				if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
				{
					AuthByCookies(args);
				}
				else
				{
					AuthByForm(args);
				}
			}
			else
			{
				throw new HttpException("Cookieless Forms Authentication is not " +
										"supported for this application.");
			}
		}

	}
}