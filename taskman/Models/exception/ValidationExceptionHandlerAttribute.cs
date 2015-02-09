using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Data.Entity.Validation;

namespace taskman.Models.exception
{
	public class ValidationExceptionHandlerAttribute : ExceptionFilterAttribute
	{
		class ErrorResponse
		{
			public string message;
			public string type = "validation";
			public Dictionary<string, string> fields;
		}

		public override void OnException(HttpActionExecutedContext context)
		{
			if (context.Exception is DbEntityValidationException)
			{
				DbEntityValidationException ex = context.Exception as DbEntityValidationException;

				var fields = new Dictionary<string, string>();

				foreach (DbEntityValidationResult result in ex.EntityValidationErrors) {					
					foreach (DbValidationError error in result.ValidationErrors)
					{
						fields.Add(error.PropertyName, error.ErrorMessage);
					}
				}

				Object response = new { error = new ErrorResponse { message = ex.Message, fields = fields } };
				context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
			}
		}
	}
}