﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SignalChat.Models;

namespace SignalChat.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SignalChatUser class
    public class SignalChatUser : IdentityUser
    {

        public virtual ICollection<Channel> Channels { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Server> Servers { get; set; }
    }
}
