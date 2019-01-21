using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace SignalRChat.Hubs
{
    public class ChatHub:Hub
    {

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message,GroupUsers);

            var ConnectionId = base.Context.ConnectionId;
        }


        private static List<string> GroupUsers = new List<string>();


        public override async Task OnConnectedAsync()
        {
            GroupUsers.Add(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, "GroupUsers");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            GroupUsers.Remove(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GroupUsers");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
