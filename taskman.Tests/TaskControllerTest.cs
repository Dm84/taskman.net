using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using taskman.Controllers;
using taskman.Models.domain;

using InputTask = taskman.Controllers.TaskController.FormattedTask;

namespace taskman.Tests
{
	public class TaskControllerTest
	{
		TaskController taskController;
		
		TaskControllerTest()
		{
			taskController = new TaskController();
		}

		[TestMethod]
		public void add()
		{
			InputTask task = new InputTask { id = 0, description = "description", completed = false, deadline = 0 };			
			taskController.Post(task);

		}
	}
}
