using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace taskman.Models.domain
{
	public class User
	{
		[Key]
		public Int32 id { get; set; }

		[Required]
		public string login { get; set; }

		[Required]		
		public string password { get; set; }
	}
}