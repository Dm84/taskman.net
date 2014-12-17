
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace taskman.Models.domain
{
	public class Task
	{
		[Key]
		public Int32 id { get; set; }

		[Required]
		public string description { get; set; }

		[Required]
		public DateTime deadline { get; set; }

		public Boolean completed { get; set; }		
	}
}