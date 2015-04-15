using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using taskman.Models.domain;

namespace taskman.Models.dao
{
	/// <summary>
	/// Инкапсулирует способ доступа к данным
	/// </summary>
    public interface ITaskDao
    {
        IEnumerable<Task> list();
        Task add(Task task);
		Task complete(int id);
		Task complete(int id, int userId);	
		IEnumerable<Task> list(int userId);

    }

}
