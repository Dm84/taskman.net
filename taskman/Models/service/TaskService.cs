using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskman.Models.dao;
using taskman.Models.domain;

namespace taskman.Models.service
{
	public class TaskService
	{
		private ITaskDao dao;

		public TaskService(ITaskDao taskDao)
		{
			dao = taskDao;
		}

		public IEnumerable<Task> list()
		{
			return dao.list();
		}

		public void add(Task task)
		{
			dao.add(task);
		}

		public void complete(int id)
		{
			dao.complete(id);
		}
	}
}