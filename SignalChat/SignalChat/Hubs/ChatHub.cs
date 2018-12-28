using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalChat.Areas.Identity.Data;
using SignalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private UserManager<SignalChatUser> _userManager;
        private SignalChatContext _dbContext;

        public ChatHub(UserManager<SignalChatUser> um, SignalChatContext dc) : base()
        {
            _userManager = um;
            _dbContext = dc;
        }

        /// <summary>
        /// Test Method
        /// </summary>
        /// <returns></returns>
        public async Task Hello()
        {
            await Clients.All.SendAsync("Hello");
        }

        public async Task JoinChannel(int channelID)
        {
            var userT = _userManager.FindByNameAsync(Context.User.Identity.Name);
            var channel = await _dbContext.Channels.Include(c => c.Users).FirstAsync(c => c.ID == channelID);
            var user = await userT;
            
            if (channel == null)
            {
                return;
            }

            if (user == null)
            {
                return;
            }

            user.CurrentChannel = channelID;
            await _userManager.UpdateAsync(user);
            
        }

        public async Task LeaveChannel(int channelID)
        {
            var userT = _userManager.FindByIdAsync(Context.UserIdentifier);
            var channel = await _dbContext.Channels.FindAsync(channelID);
            var user = await userT;

            if (channel == null)
            {
                return;
            }
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// post message
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task PostMessage(string body)
        {
            var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);

            Message msg = new Message
            {
                Content = body,
                SignalChatUserID = user.Id,
                ChannelID = user.CurrentChannel
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

        public async Task DeleteMessage(int messageID)
        {
            Message msg = await _dbContext.Messages.FindAsync(messageID);
            if (msg.SignalChatUserID != Context.UserIdentifier)
            {
                return;
            }

            _dbContext.Remove(msg);
            await _dbContext.SaveChangesAsync();
        }
    }
}
