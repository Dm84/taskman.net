using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using taskman.Models.service;
using taskman.Models.dao;

namespace taskman.Models.configuration
{
	public class Container
	{
		ITaskDao _dao = null;
		TaskService _serv = null;

		public ITaskDao getDao()
		{
			return _dao == null ? _dao = new TaskDaoEf() : _dao;
		}

		public TaskService getService(int userId)
		{
			return _serv == null ? _serv = new TaskService(getDao(), userId) : _serv;
		}
	}
}