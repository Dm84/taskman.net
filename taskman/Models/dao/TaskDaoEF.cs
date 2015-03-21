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
			using (var context = new TaskContext())
			{				
				return context.TaskSet.ToList();
			}
		}

		public Task add(Task task)
		{
			Task newTask = task;

			using (var context = new TaskContext())
			{
				newTask = context.TaskSet.Add(task);				
				context.SaveChanges();				
			}

			return newTask;
		}

		public void complete(int id)
		{
			using (var context = new TaskContext())
			{
				Task task = (from Task in context.TaskSet where Task.id == id select Task).First();
				task.completed = true;
				context.SaveChanges();
			}
		}
	}
}