using Chat.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure
{
    public class ChatDbContext : IdentityDbContext<User>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGroup>()
                .HasKey(x => new { x.UserId, x.GroupId });

            modelBuilder.Entity<UserPrivateChat>()
                .HasKey(x => new { x.UserId, x.PrivateChatId });
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
