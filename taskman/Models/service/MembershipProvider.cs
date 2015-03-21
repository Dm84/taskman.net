using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

using taskman.Models.configuration;
using taskman.Models.domain;

using UserCollection = System.Web.Security.MembershipUserCollection;
using MembershipUser = System.Web.Security.MembershipUser;

namespace taskman.Models.service
{
	public class MembershipProvider : System.Web.Security.MembershipProvider
	{
		const int MIN_PASS_LEN = 6;
		const int MAX_PASS_ATTEMPTS = 8;

		NameValueCollection _config;
		string _name, _app;

		public override void Initialize(string name, NameValueCollection config)
		{	
			_config = config;
			_name = name;
		}

		public override string ApplicationName
		{
			get { return _app; }
			set { _app = value; }
		}		

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{

			return false;
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			return false;
		}

		public override System.Web.Security.MembershipUser CreateUser(
			string username, string password, string email, string passwordQuestion, string passwordAnswer, 
			bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
		{
			using (var context = new UserContext())
			{
				var user = new User { login = username, password = password };
				context.UserSet.Add(user);
				context.SaveChanges();

				status = System.Web.Security.MembershipCreateStatus.Success;

				return new System.Web.Security.MembershipUser(
					_name, username, user.id, email, passwordQuestion, "", isApproved, false,
					new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
			}

		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			using (var context = new UserContext())
			{
				context.UserSet.Remove(new User { login = username });
			}

			return true;			
		}

		public override bool EnablePasswordReset
		{
			get { return false; }
		}

		public override bool EnablePasswordRetrieval
		{
			get { return false; }
		}

		public override System.Web.Security.MembershipUserCollection FindUsersByEmail(
			string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();

			//totalRecords = 0;
			//return new System.Web.Security.MembershipUserCollection();
		}		

		public override UserCollection FindUsersByName(
			string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();

			//totalRecords = 0;
			//var collection = new UserCollection();			
			//return collection;
		}

		public override System.Web.Security.MembershipUserCollection GetAllUsers(
			int pageIndex, int pageSize, out int totalRecords)
		{
			var collection = new UserCollection();

			using (var context = new UserContext())
			{
				IEnumerable<User> users = context.UserSet.
					Skip(pageIndex * pageSize).Take(pageSize).AsEnumerable<User>();

				totalRecords = users.Count<User>();

				foreach (User user in users)
				{
					collection.Add(new MembershipUser(_name, user.login,
						user.id, "", "", "", true, true,
						new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime()));
				}
			}

			return collection;
		}

		public override int GetNumberOfUsersOnline()
		{
			return 0;
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
		{
			using (var context = new UserContext())
			{
				var user = (from User in context.UserSet where User.login == username select User).Single<User>();

				return new MembershipUser(_name, user.login,
						user.id, "", "", "", true, true,
						new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
			}
		}

		public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			using (var context = new UserContext())
			{
				var user = context.UserSet.Find(providerUserKey);

				return new MembershipUser(_name, user.login,
						user.id, "", "", "", true, true,
						new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime());
			}
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { return MAX_PASS_ATTEMPTS; }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { return MIN_PASS_LEN; }
		}

		public override int MinRequiredPasswordLength
		{
			get { return MIN_PASS_LEN; }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override System.Web.Security.MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { return false; }
		}

		public override bool RequiresUniqueEmail
		{
			get { return false; }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(System.Web.Security.MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{
			throw new NotImplementedException();
		}
	}
}