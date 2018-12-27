using Microsoft.AspNetCore.Identity;
using SignalChat.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Models
{
    public class Server
    {
        public int ID { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }

        public virtual ICollection<IdentityRole> Roles { get; set; }
    }
}
