#define TRACE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using taskman.Models.domain;

namespace taskman.Models.dao
{
	public class TaskDaoEF : ITaskDao
	{
		public IEnumerable<Task> list()
        {
			using (var context = new TaskModelContainer())
			{				
				return context.TaskSet.ToList();
			}
		}

		public void add(Task task)
		{
			using (var context = new TaskModelContainer())
			{
				context.TaskSet.Add(task);
				context.SaveChanges();
			}
		}
	}
}