using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoChat.Models
{
	public class GroupModel
	{
		public string Name { get; set; }
		public List<UserModel> Users { get; set; }

		//Constructor
		public GroupModel()
		{
			Users = new List<UserModel>();
		}
	}
}
