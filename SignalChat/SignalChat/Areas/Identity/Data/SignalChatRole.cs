using Microsoft.AspNetCore.Identity;
using SignalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Areas.Identity.Data
{
    public class SignalChatRole : IdentityRole
    {
        public virtual ICollection<ChannelGroups> Channels { get; set; }
    }
}
