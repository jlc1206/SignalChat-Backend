using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalChat.Areas.Identity.Data;

namespace SignalChat.Models
{
    public class SignalChatContext : IdentityDbContext<SignalChatUser>
    {
        public SignalChatContext(DbContextOptions<SignalChatContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ChannelUsers>().HasKey(cu => new { cu.ChannelID, cu.UserID });
            builder.Entity<ChannelUsers>().
                HasOne(cu => cu.Channel).
                WithMany(c => c.Users).
                HasForeignKey(cu => cu.ChannelID);
            builder.Entity<ChannelUsers>().
                HasOne(cu => cu.SignalChatUser).
                WithMany(s => s.Channels).
                HasForeignKey(cu => cu.UserID);

            builder.Entity<ChannelGroups>().HasKey(cg => new { cg.ChannelID, cg.GroupID });
            builder.Entity<ChannelGroups>().
                HasOne(cg => cg.Channel).
                WithMany(c => c.Groups).
                HasForeignKey(cg => cg.ChannelID);
            builder.Entity<ChannelGroups>().
                HasOne(cg => cg.Group).
                WithMany(g => g.Channels).
                HasForeignKey(cg => cg.GroupID);

        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelUsers> ChannelUsers { get; set; }
        public DbSet<ChannelGroups> ChannelGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
