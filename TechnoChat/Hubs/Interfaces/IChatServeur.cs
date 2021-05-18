using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoChat.Hubs.Interfaces
{
	public interface IChatServeur
	{
		//Task SendMessage(string user, string message);
		//Task ChangeStatut();
		//Task MemberJoinGroup(string groupName);
		//Task MemberLeaveGroup(string groupName);
		//Task Wizz(string user);

        /// <summary>
        /// Permet d'envoyer un message à tous les users ou à un user particulier ou à un groupe
        /// </summary>
        /// <param name="message">Le message a transmettre</param>
        /// <param name="recipient">L'utilisateur spécifique ( par défaut : Tous)</param>
        /// <param name="groupName">Le groupe spécifique ( par défaut : Aucun)</param>
        /// <returns>La Task</returns>
        Task SendMessage(string message, string recipient = "Tous", string groupName = "Aucun");
        /// <summary>
        /// Permet de demander son changement de status
        /// </summary>
        /// <param name="status">le status de l'utilisateur <seealso cref="EStatus"/></param>
        /// <returns>La Task</returns>
        Task ChangeStatus(EStatus status);
        /// <summary>
        /// Permet de rejoindre un groupe
        /// </summary>
        /// <param name="groupName">Le nom du groupe</param>
        /// <returns>La Task</returns>
        Task MemberJoinGroup(string groupName);
        /// <summary>
        /// Permet de quitter un groupe
        /// </summary>
        /// <param name="groupName">Le nom du groupe</param>
        /// <returns>La Task</returns>
        Task MemberLeaveGroup(string groupName);
        /// <summary>
        /// Permet d'attirer l'attention d'un utilisateur ou d'un groupe
        /// </summary>
        /// <param name="recipient">L'utilisateur spécifique ( par défaut : Tous)</param>
        /// <param name="groupName">Le groupe spécifique ( par défaut : Aucun)</param>
        /// <returns>La Task</returns>
        Task Wizz(string recipient = "Tous", string groupName = "Aucun");
    }

	public enum EStatus
	{
		Online,
		Offline,
		DotNotDisturb
	}
}