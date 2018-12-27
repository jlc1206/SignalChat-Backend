using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalChat.Areas.Identity.Data;
using SignalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Hubs
{
    public class ChatHub : Hub
    {
        private UserManager<SignalChatUser> _userManager;

        public ChatHub(UserManager<SignalChatUser> um) : base()
        {
            _userManager = um;
        }

        public async Task Hello()
        {
            await Clients.All.SendAsync("Hello");
        }

        public async Task PostMessage(int channel, string body)
        {        
            Message msg = new Message {
                Content = body,
                SignalChatUserID = Context.UserIdentifier,
                ChannelID = channel
            };


        }
    }
}
