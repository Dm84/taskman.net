using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace taskman.Models.domain
{
	public class TaskContext : DbContext
	{
		public TaskContext() : base("TaskConnection") 
		{
			Database.SetInitializer<TaskContext>(new DropCreateDatabaseIfModelChanges<TaskContext>());
		}

		public DbSet<Task> TaskSet { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}