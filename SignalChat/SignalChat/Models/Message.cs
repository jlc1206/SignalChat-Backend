using SignalChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Content { get; set; }

        public string SignalChatUserID { get; set; }
        public virtual SignalChatUser SignalChatUser { get; set; }

        public int ChannelID { get; set; }
        public virtual Channel Channel { get; set; }
    }
}
