using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalChat.Models
{
    public class Server
    {
        public int ID;
        
        public virtual ICollection<Channel> Channels { get; set; }

        public virtual ICollection<IdentityRole> Roles { get; set; }
    }
}
