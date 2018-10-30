using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Hello()
        {
            await Clients.All.SendAsync("Hello");
        }
    }
}
