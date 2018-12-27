using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private SignalChatContext _dbContext;

        public ChatHub(UserManager<SignalChatUser> um, SignalChatContext dc) : base()
        {
            _userManager = um;
            _dbContext = dc;
        }

        public async Task Hello()
        {
            await Clients.All.SendAsync("Hello");
        }

        public async Task JoinChannel(int channelID)
        {
            var userID = Context.UserIdentifier;
        }

        /// <summary>
        /// post message to channel
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task PostMessage(int channel, string body)
        {        
            Message msg = new Message {
                Content = body,
                SignalChatUserID = Context.UserIdentifier,
                ChannelID = channel
            };

            _dbContext.Messages.Add(msg);
            _dbContext.SaveChanges();
        }
    }
}
