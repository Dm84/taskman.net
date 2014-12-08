using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using taskman.Models.beans;
using taskman.Models.service;
using taskman.Models.domain;

namespace taskman.Controllers
{
    public class TaskController : ApiController
    {
		public class FormattedTask {
			public int id;
			public string description;
			public long deadline;			
			public Boolean completed;

			public FormattedTask()
			{

			}

			public FormattedTask(Task src) {
				Int64 timestamp = (long) (src.deadline - (new DateTime(1970, 1, 1))).TotalSeconds * 1000L;
				id = src.id; 
				description = src.description;
				deadline = timestamp; 
				completed = src.completed;
			}

			public static implicit operator Task(FormattedTask src) {
				DateTime deadline = (new DateTime(1970, 1, 1)).AddSeconds((double)(src.deadline / 1000L));
				return new Task { id = src.id, description = src.description, deadline = deadline, completed = src.completed };
			}
		}

		private TaskService serv;

		public TaskController()
		{
			serv = BeanFactory.createService();
		}

        // GET endpoint/tasks/
		[HttpGet]
		public IEnumerable<FormattedTask> Get()
        {			
            return serv.list().Select(delegate(Task src) {
				return new FormattedTask(src); 
			});
        }

        // POST endpoint/tasks/
		[HttpPost]
		public void Post([FromBody] FormattedTask task)
		{
			serv.add(task);
		}
    }
}