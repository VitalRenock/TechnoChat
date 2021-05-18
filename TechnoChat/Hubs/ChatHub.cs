using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TechnoChat.Infrastructure.Interfaces;
using TechnoChat.Hubs.Interfaces;

using TII = TechnoChat.Infrastructure.Interfaces;

namespace TechnoChat.Hubs
{
	public class ChatHub : Hub<IChatClient>, IChatServeur
	{
		private TII.IGroupManager groupManager;

		#region Constructor
		
		public ChatHub(TII.IGroupManager groupManager)
		{
			this.groupManager = groupManager;
		}

		#endregion

		public async Task MemberJoinGroup(string groupName)
		{
			string connectionId = Context.ConnectionId;
			string user = Context.User.Claims.First(s => s.Type == ClaimTypes.GivenName).Value;

			await Groups.AddToGroupAsync(Context.ConnectionId, groupName).ContinueWith((T) => 
			{
				groupManager.AddUserToGroup(user, connectionId, groupName);
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="recipient"></param>
		/// <param name="groupName"></param>
		/// <returns></returns>
		/// <remarks>En Js, il FAUT transmettre TOUT les paramètres même ceux par défault</remarks>
		public async Task SendMessage(string message, string recipient = "Tous", string groupName = "Aucun")
		{
			string user = Context.User.Claims.First(s => s.Type == ClaimTypes.GivenName).Value;
			
			if (recipient != "Tous")
				await Clients.Client(recipient).ReceiveMessage($"{user} : {message}", Context.ConnectionId);
			else if (groupName != "Aucun")
				await Clients.Group(groupName).ReceiveMessage($"{user} : {message}", Context.ConnectionId);
			else
				await Clients.AllExcept(Context.ConnectionId).ReceiveMessage($"{user} : {message}", Context.ConnectionId);
		}

		#region Override 'Hub' Methods
		
		public override Task OnConnectedAsync()
		{
			Clients.AllExcept(Context.ConnectionId).ReceiveMessage($"{Context.ConnectionId} join the TechnoChat", Context.ConnectionId);
			return base.OnConnectedAsync();
		}
		public override Task OnDisconnectedAsync(Exception exception)
		{
			Clients.AllExcept(Context.ConnectionId).ReceiveMessage($"{Context.ConnectionId} has leaved the TechnoChat", Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}

		#endregion

		#region NOT IMPLEMENTED

		public Task ChangeStatus(EStatus status)
		{
			throw new NotImplementedException();
		}
		public async Task MemberLeaveGroup(string groupName)
		{
			await Task.CompletedTask;
		} 
		public Task Wizz(string recipient = "Tous", string groupName = "Aucun")
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
