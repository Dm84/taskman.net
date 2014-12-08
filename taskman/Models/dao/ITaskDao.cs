using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using taskman.Models.domain;

namespace taskman.Models.dao
{
    public interface ITaskDao
    {
        IEnumerable<Task> list();
        void add(Task task);
		void complete(int id);
    }

}
