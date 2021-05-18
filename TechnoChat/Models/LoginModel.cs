using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace TechnoChat.Models
{
	public class LoginModel
	{
		[Required]
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
