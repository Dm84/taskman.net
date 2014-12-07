using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskman.Models.domain;

namespace taskman.Models.dao
{
	public class TaskDaoEF : ITaskDao
	{
		public IEnumerable<Task> list()
        {
			using (TaskModelContainer context = new TaskModelContainer())
			{
				IQueryable<Task> tasksQuery = from task in context.Tasks select task;
				return tasksQuery;
			}
		}

		public void add(Task task)
		{
			using (TaskModelContainer context = new TaskModelContainer())
			{
				context.Tasks.Add(task);
				context.SaveChanges();
			}
		}
	}
}