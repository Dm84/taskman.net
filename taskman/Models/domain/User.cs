using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskman.Models.domain
{
	public class User
	{
		[Key]
		public Int32 id { get; set; }

		[Required]
		[Index("IX_login", 1, IsUnique = true)]
		[MaxLength(64)]
		public string login { get; set; }

		[Required]		
		public string password { get; set; }
	}
}