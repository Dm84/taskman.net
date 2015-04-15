using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Validation;

using System.Web.Security;

using taskman.Models.configuration;
using taskman.Models.service;
using taskman.Models.domain;
using taskman.Models.exception;

namespace taskman.Controllers
{
	public class TaskController : ApiController
	{
		/// <summary>
		/// представление задания для фронтенда
		/// </summary>
		public class FrontendTask
		{
			public Int32 id;
			public string description;
			public long deadline;
			public Boolean completed;

			public FrontendTask()
			{
			}

			public FrontendTask(Task src)
			{
				Int64 timestamp = (long)(src.deadline - (new DateTime(1970, 1, 1))).TotalSeconds * 1000L;
				id = src.id;
				description = src.description;
				deadline = timestamp;
				completed = src.completed;
			}

			public static implicit operator Task(FrontendTask src)
			{
				DateTime deadline = (new DateTime(1970, 1, 1)).AddSeconds((double)(src.deadline / 1000L));
				return new Task { id = src.id, description = src.description, deadline = deadline, completed = src.completed };
			}
		}

		private Container _container;

		public TaskController()
		{
			_container = new Container();			
		}

		private TaskService GetTaskService() {

			var userId = Membership.GetUser().ProviderUserKey as int?;

			if (userId != null)
			{
				return _container.getService(userId.Value);
			} 
			else
			{
				throw new InvalidOperationException();
			}

		}

		// GET endpoint/tasks/
		[HttpGet]
		public IEnumerable<FrontendTask> Get()
		{	
			return GetTaskService().list().Select((Task src) =>
			{
				return new FrontendTask(src);
			});
		}

		//PATCH endpoint/tasks/5
		[HttpPatch]
		public Object Patch(int id)
		{
			TaskService serv = GetTaskService();
			GetTaskService().complete(id);

			return new { completed = true };
		}


		// POST endpoint/tasks/
		[HttpPost]
		[ValidationExceptionHandler]
		public FrontendTask Post([FromBody] FrontendTask task)
		{
			return new FrontendTask(GetTaskService().add(task));
		}

		// PUT endpoint/tasks/5/complete 
		[HttpPut]
		public Object Complete(int id)
		{
			GetTaskService().complete(id);
			return new { completed = true };
		}
	}
}