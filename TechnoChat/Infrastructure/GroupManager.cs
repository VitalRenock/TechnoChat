using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TechnoChat.Models;
using TII = TechnoChat.Infrastructure.Interfaces;

namespace TechnoChat.Infrastructure
{
	public class GroupManager: TII.IGroupManager
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
			try
			{
				GroupModel gm = groups.FirstOrDefault(c => c.Name == groupName);
				if (gm != null)
				{
					UserModel um = gm.Users.FirstOrDefault(u => u.Name == user);
					if (um != null)
					{
						gm.Users.Remove(um);
					}
				}
				return true;
			}
			catch (Exception)
			{

				return false;
			}

		}

		public List<UserModel> ListUserOfGroup(string groupName)
		{
			return groups.FirstOrDefault(c => c.Name == groupName)?.Users;
		}

		public List<GroupModel> ListOfGroup()
		{
			return groups;
		}
	}
}
