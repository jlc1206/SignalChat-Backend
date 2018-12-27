﻿using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Test Method
        /// </summary>
        /// <returns></returns>
        public async Task Hello()
        {
            await Clients.All.SendAsync("Hello");
        }

        /// <summary>
        /// post message
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task PostMessage(int channelID, string body)
        {
            var user = await _userManager.FindByIdAsync(Context.UserIdentifier);

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
