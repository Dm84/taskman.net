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
		private TaskService serv;

		public TaskController()
		{
			serv = BeanFactory.createService();
		}

        // GET endpoint/tasks/
		[HttpGet]
        public IEnumerable<Task> Get()
        {			
            return serv.list();
        }

        // POST endpoint/tasks/
		[HttpPost]
        public void Post([FromBody] Task task)
        {
			serv.add(task);
        }
    }
}
