using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public void RegisterGroup(string name)
        {
            Database1Entities db = new Database1Entities();
            db.Groups.Add(new Group { Name = name });
            db.SaveChanges();
        }
        public void RegisterClient(string group)
        {
            string connectionGUID = Context.ConnectionId;
            Database1Entities db = new Database1Entities();
            var dbGroup = db.Groups.FirstOrDefault(x => x.Name == group);
            db.Connections.Add(new Connection
            {
                ConnectionGUID = connectionGUID,
                GroupId = dbGroup.GroupId
            });
            db.SaveChanges();
            Groups.Add(connectionGUID, group);
        }
        public void SendMessage(string name, string message)
        {
            Database1Entities db = new Database1Entities();
            var connectionGUID = Context.ConnectionId;
            var dbGroup = db.Groups.FirstOrDefault(x => x.Connections.Any(a => a.ConnectionGUID == connectionGUID));
            if(dbGroup != null)
                Clients.Group(dbGroup.Name).addNewMessageToPage(new ChatMessage() { UserName = name, Message = message });
        }
    }
    public class ChatMessage
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}