using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskman.Models.domain;

namespace taskman.Models.dao
{
	public class TaskDaoEF : ITaskDao
	{
		IEnumerable<Task> list()
        {
			using (TaskModelContainer context = new TaskModelContainer())
			{
				IQueryable<Task> tasksQuery = from task in context.Tasks select task;
				return tasksQuery;
			}
		}

		void add(Task task)
		{
			using (TaskModelContainer context = new TaskModelContainer())
			{
				context.Tasks.Add(task);
				context.SaveChanges();
			}
		}
	}
}