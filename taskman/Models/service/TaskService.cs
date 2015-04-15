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
		private int userId;

		public TaskService(ITaskDao taskDao, int userId)
		{
			this.dao = taskDao;
			this.userId = userId; 
		}

		public IEnumerable<Task> list()
		{
			return dao.list(userId);
		}

		public Task add(Task task)
		{
			task.user_id = userId;
			return dao.add(task);
		}

		public void complete(int id)
		{
			dao.complete(id, userId);
		}
	}
}