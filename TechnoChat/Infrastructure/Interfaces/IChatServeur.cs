using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoChat.Infrastructure.Interfaces
{
	public interface IChatServeur
	{
		Task SendMessage(string user, string message);

		Task ChangeStatut();

		Task MemberJoinGroup(string groupName);

		Task MemberLeaveGroup(string groupName);

		Task Wizz(string user);
	}

	public enum EStatus
	{
		Online,
		Offline,
		DotNotDisturb
	}
}