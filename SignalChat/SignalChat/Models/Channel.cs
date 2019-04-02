using Microsoft.AspNetCore.Identity;
using SignalChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Models
{
    public class Channel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public bool EnableWhitelist { get; set; }
        public virtual ICollection<ChannelUsers> Users { get; set; }        
        public virtual ICollection<ChannelGroups> Groups { get; set; }
    }
}
