using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using taskman.Models.domain;
using taskman.Models.configuration;
using taskman.Models.service;

namespace taskman.Tests
{
	[TestClass]
	public class TaskServiceTest
	{
		Container _container;

		public TaskServiceTest()
		{
			_container = new Container();			
		}

		[TestMethod]
		public void add()
		{
			var serv = _container.getService();
			Task task = new Task { id = 0, description = "test task", completed = false, deadline = DateTime.Now };
			Task newTask = serv.add(task);

			Assert.AreNotEqual(newTask.id, 0);
		}

		[TestMethod]
		public void list()
		{
			var serv = _container.getService();
			IEnumerable<Task> tasks = serv.list();
			Assert.AreNotEqual(tasks.GetEnumerator().MoveNext(), false);
		}

	}
}
