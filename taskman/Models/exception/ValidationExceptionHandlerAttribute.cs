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
		class ValidationResponse
		{
			public string error;
			public IEnumerable<DbEntityValidationResult> fields;
		}

		public override void OnException(HttpActionExecutedContext context)
		{
			if (context.Exception is DbEntityValidationException)
			{
				DbEntityValidationException ex = context.Exception as DbEntityValidationException;

				context.Response = context.Request.CreateResponse(
					HttpStatusCode.InternalServerError, new ValidationResponse { error = ex.Message, fields = ex.EntityValidationErrors });
			}
		}
	}
}