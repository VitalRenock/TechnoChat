using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoChat.Hubs.Interfaces
{
    /// <summary>
    /// Interface conditionnant le côté client de SignalR
    /// </summary>
    public interface IChatClient
	{
        /// <summary>
        /// Permet de recevoir un message individuel ou de groupe
        /// </summary>
        /// <param name="message">Le message envoyé</param>
        /// <returns>La task</returns>
        Task ReceiveMessage(string message, string user, string groupName = "");
        /// <summary>
        /// Permet de recevoir le changement de status d'un utilisateur
        /// </summary>
        /// <param name="status">le status de l'utilisateur <seealso cref="EStatus"/></param>
        /// <param name="user">L'utilisateur ayant changé de status</param>
        /// <returns>La Task</returns>
        Task StatusChanged(EStatus status, string user);
        /// <summary>
        /// Permet de savoir lorsqu'un utilisateur rejoint un groupe
        /// </summary>
        /// <param name="groupName">Le nom du groupe</param>
        /// <param name="user">L'utilisateur</param>
        /// <returns>La Task</returns>
        Task MemberHasJoinGroup(string groupName, string user);
        /// <summary>
        /// Permet de savoir lorsqu'un utilisateur quitte un groupe
        /// </summary>
        /// <param name="groupName">Le nom du groupe</param>
        /// <param name="user">L'utilisateur</param>
        /// <returns>La Task</returns>
        Task MemberHasLeaveGroup(string groupName, string user);
        /// <summary>
        /// Permet d'attire l'attention d'un utilisateur
        /// </summary>
        /// <param name="user">L'utilisateur ayant lancé le wizz</param>
        /// <returns>La Task</returns>
        Task Wizz(string user);
        /// <summary>
        /// Permet d'informer de l'arrivée d'un nouveau membre
        /// </summary>
        /// <param name="user">L'utilisateur </param>
        /// <returns>La Task</returns>
        Task MemberJoinChat(string user);
        /// <summary>
        /// Permet d'informer du départ d'un membre
        /// </summary>
        /// <param name="user">L'utilisateur </param>
        /// <returns>La Task</returns>
        Task MemberLeaveChat(string user);
    }
}