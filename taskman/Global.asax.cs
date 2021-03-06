﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using System.Web.Security;

using MvcFlash.Core;
using MvcFlash.Core.Extensions;


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
				get { return "FormAuthentication"; }
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

		private void Auth(HttpContext context, FormsAuthenticationTicket ticket)
		{
			string[] userdata = ticket.UserData.Split(',');

			context.User = new System.Security.Principal.GenericPrincipal(
			  new UserIdentity(ticket),
			  new string[] { userdata[0] });

			//args.Context.User = new System.Security.Principal.GenericPrincipal(
			//			new System.Security.Principal.GenericIdentity(username, "formAuth"),
			//			new string[] { "user" });

		}

		private void AuthByCookies(FormsAuthenticationEventArgs args)
		{
			try
			{
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
				  Request.Cookies[FormsAuthentication.FormsCookieName].Value);

				Auth(args.Context, ticket);
			}
			catch
			{
				this.Application["error_msg"] = "Некорректные куки";
			}
		}

		private void ValidateUser(FormsAuthenticationEventArgs args, string username, string password)
		{
			if (Membership.ValidateUser(username, password))
			{
				MembershipUser user = Membership.GetUser(username);

				var ticket = new FormsAuthenticationTicket(1, username, new DateTime(), 
					(new DateTime()).AddHours(100), true, "user," + user.ProviderUserKey.ToString());

				Auth(args.Context, ticket);

				HttpCookie cookie = new HttpCookie(
					FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

				cookie.HttpOnly = true;
				Response.Cookies.Add(cookie);
			}
			else
			{
				this.Application["error_msg"] = "Неверный логин или пароль";
			}
		}

		public void AuthByForm(FormsAuthenticationEventArgs args, string action)
		{
			string username = this.Request.Params["username"], password = this.Request.Params["password"];

			try
			{
				if (action == "signup")
				{
					Membership.CreateUser(username, password);
				}

				ValidateUser(args, username, password);
			}								
			catch (Exception e) {

				if (e is MembershipCreateUserException)
				{
					var cuException = e as MembershipCreateUserException;

					switch (cuException.StatusCode)
					{
						case MembershipCreateStatus.DuplicateUserName:
							this.Application["error_msg"] = "Пользователь с таким логином уже зарегистрирован";
							break;
						case MembershipCreateStatus.InvalidUserName:
						case MembershipCreateStatus.InvalidPassword:
							this.Application["error_msg"] = "Некорректный логин или пароль";
							break;

						default:
							this.Application["error_msg"] = "Не удалось зарегистрировать с данным логином и паролем";
							break;
					}				
				}
				else
				{
					this.Application["error_msg"] = "Неизвестная ошибка входа";
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
					string action = this.Request.Params["action"];
					string username = this.Request.Params["username"];

					if (username != null && username.Count() > 0)
					{
						AuthByForm(args, action);
					}
				}
			}
			else
			{
				this.Application["error_msg"] = "Для входа браузер должен поддерживать куки";
			}
		}

	}
}