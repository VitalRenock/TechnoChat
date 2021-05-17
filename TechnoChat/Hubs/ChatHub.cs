using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TechnoChat.Infrastructure.Interfaces;

namespace TechnoChat.Hubs
{
	public class ChatHub : Hub<IChatClient>, IChatServeur
	{
		TechnoChat.Hubs.IGroupManager groupManager1;
		public ChatHub(IGroupManager groupManager)
		{
			groupManager1 = groupManager;
		}

		public Task ChangeStatut()
		{
			throw new NotImplementedException();
		}

		public async Task MemberJoinGroup(string groupName)
		{
			string connectionId = Context.ConnectionId;

			string user = Context.User.Claims.First(s => s.Type == ClaimTypes.GivenName).Value;

			

			await Groups.AddToGroupAsync(Context.ConnectionId, groupName).ContinueWith((T) => 
			{
				groupManager1.AddUserToGroup(user, connectionId, groupName);
			});

			//await Clients.Group(groupName).ReceiveMessage($"{Context.ConnectionId} à rejoint {groupName}");
		}

		public async Task MemberLeaveGroup(string groupName)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
			await Clients.Group(groupName).ReceiveMessage($"{Context.ConnectionId} à quitter {groupName}");
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.All.ReceiveMessage($"{user}:{message}");
		}

		public Task Wizz(string user)
		{
			throw new NotImplementedException();
		}

		public override Task OnConnectedAsync()
		{
			Clients.All.ReceiveMessage("Utilisateur connecté");
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			Clients.All.ReceiveMessage("Utilisateur déconnecté");
			return base.OnDisconnectedAsync(exception);
		}
	}
}
