using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyMvc
{
	public class UserEducationViewModel
	{
		public Guid Guid { get; set; }

		public string Level { get; set; }

		public string School { get; set; }

		public string SchoolYear { get; set; }
	}
}
