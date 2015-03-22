using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace taskman.Models.domain
{
	public class TaskmanContext : DbContext
	{
		public TaskmanContext() : base("TaskConnection") 
		{
			Database.SetInitializer<TaskmanContext>(new DropCreateDatabaseIfModelChanges<TaskmanContext>());
		}

		public DbSet<Task> TaskSet { get; set; }
		public DbSet<User> UserSet { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}