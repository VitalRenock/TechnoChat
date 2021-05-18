using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoChat.Models;

namespace TechnoChat.Infrastructure.Interfaces
{
	public interface IGroupManager
	{
		bool AddUserToGroup(string user, string connectionId, string groupName);
		bool RemoveUserFromGroup(string user, string groupName);
		List<UserModel> ListUserOfGroup(string groupName);
		List<GroupModel> ListOfGroup();
	}
}
