using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskman.Models.service;
using taskman.Models.dao;

namespace taskman.Models.beans
{
	public class BeanFactory
	{
		public static TaskService createService()
		{
			ITaskDao dao = new TaskDaoEF();
			return new TaskService(dao);
		}
	}
}