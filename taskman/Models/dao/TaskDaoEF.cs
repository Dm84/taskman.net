using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using taskman.Models.domain;

namespace taskman.Models.dao
{
	/// <summary>
	/// Доступ к данным на основе entity framework codefirst
	/// </summary>
	public class TaskDaoEf : ITaskDao
	{
		public IEnumerable<Task> list()
        {
			using (var context = new TaskmanContext())
			{				
				return context.TaskSet.ToList();
			}
		}

		public IEnumerable<Task> list(int userId)
		{
			using (var context = new TaskmanContext())
			{
				return (from Task in context.TaskSet where Task.user_id == userId select Task).ToList();
			}
		}

		public Task add(Task task)
		{
			Task newTask = task;

			using (var context = new TaskmanContext())
			{
				newTask = context.TaskSet.Add(task);
				context.SaveChanges();				
			}

			return newTask;
		}

		public Task complete(int id)
		{
			using (var context = new TaskmanContext())
			{
				Task task = (from Task in context.TaskSet where Task.id == id select Task).First();
				task.completed = true;
				context.SaveChanges();

				return task;
			}		
		}

		public Task complete(int id, int userId)
		{
			using (var context = new TaskmanContext())
			{
				Task task = (from Task in context.TaskSet where Task.id == id select Task).First();

				if (task.user_id == userId)
				{
					task.completed = true;
					context.SaveChanges();
				}

				return task;
			}
		}

	}
}