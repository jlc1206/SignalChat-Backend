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
            var userId = Context.UserIdentifier;
            var CU = await _dbContext.ChannelUsers.SingleOrDefaultAsync(cu => cu.UserID == userId && cu.ChannelID == channelID);
            var channel = await _dbContext.Channels.SingleOrDefaultAsync(c => c.ID == channelID);

            if (userId == null)
            {
                return;
            }

            if (CU.Channel.EnableWhitelist)
            {
                var CG = await _dbContext.ChannelGroups.Where(cg => cg.ChannelID == channelID).ToListAsync();
                var hasRole = false;
                foreach(var group in CG)
                {
                    if (Context.User.IsInRole(group.Group.Name))
                    {
                        hasRole = true;
                    }
                }
                if (hasRole || CU.Status == 1)
                {
                    CU.isJoined = true;
                    _dbContext.Entry(CU).State = EntityState.Modified;
                }
                else
                {
                    return;
                }
            }
            else
            {
                CU = new ChannelUsers { ChannelID = channelID, UserID = userId, isJoined = true, Status = 0 };
                _dbContext.Add(CU);
            }            

            await _dbContext.SaveChangesAsync();

        }

        public async Task GetMessages()
        {
            var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
            var messages = await _dbContext.Messages.Where(m => m.ChannelID == user.CurrentChannel).OrderByDescending(m => m.ID).Select(m => new { ID = m.ID, Content = m.Content, UserID = m.SignalChatUserID }).ToArrayAsync();
            await Clients.Caller.SendAsync("LoadMessages", messages);
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
