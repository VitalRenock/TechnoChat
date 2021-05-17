using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoChat.Models;

namespace TechnoChat.Hubs
{
	public class GroupManager: Hub, IGroupManager
	{
		private readonly List<GroupModel> groups;

		public GroupManager()
		{
			groups = new List<GroupModel>();
		}
			 


		public bool AddUserToGroup(string user, string connectionId, string groupName)
		{
			try
			{
				UserModel userModel = new UserModel() { ConnectionID = connectionId, Name = user };
				if (groups.Count(c => c.Name == groupName) > 1)
				{
					groups.Add(new GroupModel() { Name = groupName });
				}
				groups.First(c => c.Name == groupName).Users.Add(userModel);
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public bool RemoveUserFromGroup(string user, string groupName)
		{
			GroupModel groupModel = groups.FirstOrDefault(c => c.Name == groupName);
			if (groupModel != null)
			{
				groups.Remove(groupModel);
				return true;
			}
			else
				return false;
		}

		public List<UserModel> ListUserOfGroup(GroupModel group)
		{
			return groups.FirstOrDefault(c => c.Name == group.Name)?.Users;
		}

		public List<GroupModel> ListOfGroup()
		{
			return groups;
		}
	}
}
