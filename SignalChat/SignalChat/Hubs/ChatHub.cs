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

        /// <summary>
        /// Subscribe to a channel
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public async Task SubscribeChannel(int channelID)
        {
            var userT = _dbContext.Users.FindAsync(Context.UserIdentifier);
            var channelT = _dbContext.Channels.FindAsync(channelID);

            var user = await userT;
            var channel = await channelT;
            user.Channels.Add(channel);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Unsubscribe to a channel
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public async Task UnsubscribeChannel(int channelID)
        {
            var userT = _dbContext.Users.FindAsync(Context.UserIdentifier);
            var channelT = _dbContext.Channels.FindAsync(channelID);

            var user = await userT;
            var channel = await channelT;

            user.Channels.Remove(channel);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// post message to specific channel
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task PostMessage(int channelID, string body)
        {
            Message msg = new Message
            {
                Content = body,
                SignalChatUserID = Context.UserIdentifier,
                ChannelID = channelID
            };

            _dbContext.Messages.Add(msg);
            _dbContext.SaveChanges();
        }

        public async Task EditMessage(int messageID, string body)
        {
            Message msg = await _dbContext.Messages.FindAsync(messageID);
            if (msg.SignalChatUserID != Context.UserIdentifier)
            {
                return;
            }

            msg.Content = body;
            _dbContext.Add(msg);
            await _dbContext.SaveChangesAsync();
        }
    }
}
