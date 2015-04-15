using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace taskman.Controllers
{
    public class FrontendController : Controller
    {
        //
        // GET: /Frontend/

        public ActionResult Index()
        {
            return View();
        }

		public class LoginModel
		{
			public string	signin_url;
			public string	login;
			public string[]	errors;
			public string	login_input_name;
			public string	pwd_input_name;
			public string	login_placeholder;
			public string	pwd_placeholder;
			public string	pwd_confirm_placeholder;
			public string	pwd_confirm_req_msg;
			public string	pwd_len_req_msg;

			public int pwd_min_len;

			public string	field_req_msg;
			public string	signup_header;
			public string	signin_header;

			public string	signup_url;

			public string	signin_btn_caption;
			public string	signup_btn_caption;

			public string signin_btn_name;
			public string signup_btn_name;
			public string signin_btn_val;
			public string signup_btn_val;

			public string error_msg;
		}

		private string GetFlashError()
		{
			var error = "";
			if (HttpContext.Application["error_msg"] != null)
			{
				error = HttpContext.Application["error_msg"] as string;
				HttpContext.Application.Remove("error_msg");
			}

			if (MvcFlash.Core.Flash.Instance.Count != 0)
			{
				error = MvcFlash.Core.Flash.Instance.Pop().Content;
			}

			return error;
		}

		public ActionResult Login()
		{
			string login = this.Request.Params["username"] != null ? this.Request.Params["username"] : "";

			var signin_url = FormsAuthentication.GetRedirectUrl(login, false);
			signin_url = signin_url == null ? FormsAuthentication.DefaultUrl : signin_url;			

			return View(new LoginModel { 
				signin_url = signin_url,
				field_req_msg = "Обязательное поле",
				login = login,
				login_input_name = "username",
				login_placeholder = "Ваш логин",
				pwd_placeholder = "Ваш пароль",
				pwd_confirm_placeholder = "Ваш пароль еще раз",
				pwd_confirm_req_msg = "Пароли должны совпадать",
				pwd_min_len = Membership.MinRequiredPasswordLength,
				pwd_len_req_msg = "Длина пароля должна быть не менее " + Membership.MinRequiredPasswordLength.ToString() + " символов",
				pwd_input_name = "password",
				signin_btn_caption = "Войти",
				signup_btn_caption = "Зарегистрироваться",
				signin_header = "Авторизация",
				signup_header = "Регистрация",
				signin_btn_name = "action",
				signup_btn_name = "action",
				signin_btn_val = "signin",
				signup_btn_val = "signup",
				error_msg = GetFlashError()
			});
		}

    }
}
