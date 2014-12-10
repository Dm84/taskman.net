using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using taskman.Models.domain;
using taskman.Models.beans;
using taskman.Models.service;

namespace taskman.Tests
{
	[TestClass]
	public class TaskServiceTest
	{
		TaskService serv;

		public TaskServiceTest()
		{
			serv = BeanFactory.createService();
		}

		[TestMethod]
		public void add()
		{
			Task task = new Task { id = 0, description = "test task", completed = false, deadline = DateTime.Now };
			Task newTask = serv.add(task);

			Assert.AreNotEqual(newTask.id, 0);
		}

		[TestMethod]
		public void list()
		{
			IEnumerable<Task> tasks = serv.list();
			Assert.AreNotEqual(tasks.GetEnumerator().MoveNext(), false);
		}

	}
}
