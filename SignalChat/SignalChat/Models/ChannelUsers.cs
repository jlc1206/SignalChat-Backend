using SignalChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SignalChat.Models
{
    public class ChannelUsers
    {
        [DefaultValue(false)]
        public bool isJoined { get; set; }

        [DefaultValue(0)]
        public int Status { get; set; }

        public int ChannelID { get; set; }

        public virtual Channel Channel { get; set; }

        public string UserID { get; set; }

        public virtual SignalChatUser SignalChatUser { get; set; }
    }
}
