using Microsoft.AspNetCore.Identity;
using SignalChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Models
{
    public class ChannelGroups
    {
        public int ChannelID { get; set; }

        public virtual Channel Channel { get; set; }

        public string GroupID { get; set; }

        public virtual SignalChatRole Group { get; set; }
    }
}
