using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoChat.Models;

namespace TechnoChat.Hubs
{
	public interface IGroupManager
	{
		//bool AddUserToGroup(GroupModel group, UserModel user);
		bool AddUserToGroup(string user, string connectionId, string groupName);
		//void RemoveUserFromGroup(GroupModel group, UserModel user);
		bool RemoveUserFromGroup(string user, string groupName);
		List<UserModel> ListUserOfGroup(GroupModel group);
		List<GroupModel> ListOfGroup();
	}
}
