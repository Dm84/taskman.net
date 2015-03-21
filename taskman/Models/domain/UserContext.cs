using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace taskman.Models.domain
{
	public class UserContext : DbContext
	{
		public UserContext()
			: base("TaskConnection")
		{
			Database.SetInitializer<UserContext>(new CreateDatabaseIfNotExists<UserContext>());
		}

		public DbSet<User> UserSet { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}