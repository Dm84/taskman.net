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
			public string	login_param;
			public string	pwd_param;
			public string	login_placeholder;
			public string	pwd_placeholder;
			public string	pwd_confirm_placeholder;
			public string	pwd_confirm_req_msg;
			public string	pwd_len_req_msg;

			public string	field_req_msg;
			public string	signup_header;
			public string	signin_header;

			public string	signup_url;

			public string	signin_btn_caption;
			public string	signup_btn_caption;
		}

		public ActionResult Login()
		{
			//ViewData["signin_url"] = Url.Action("Login");
			//ViewData["login"] = "";

			string username = this.Request.Params["username"], password = this.Request.Params["password"];

			if (username != null)
			{
				if (Membership.ValidateUser(username, password))
				{
					FormsAuthentication.RedirectFromLoginPage(username, true);
				}
			}			

			return View(new LoginModel { 
				signin_url = Url.Action("Login"),
				field_req_msg = "Обязательное поле",
				login = "",
				login_param = "username",
				login_placeholder = "Ваш логин",
				pwd_placeholder = "Ваш пароль",
				pwd_confirm_placeholder = "Ваш пароль еще раз",
				pwd_confirm_req_msg = "Пароли должны совпадать",
				pwd_len_req_msg = "Длина пароля должна быть не менее:",
				pwd_param = "password",
				signin_btn_caption = "Войти",
				signup_btn_caption = "Зарегистрироваться",
				signin_header = "Авторизация",
				signup_header = "Регистрация"
			});
		}

    }
}
