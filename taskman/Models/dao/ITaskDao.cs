using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using taskman.Models.domain;

namespace taskman.Models.dao
{
    interface ITaskDao
    {
        public IEnumerable<Task> list();
        public void add();
    }
}
