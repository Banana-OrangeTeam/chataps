using Chat.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Chat.Infrastructure.Entities
{
    public class User: IdentityUser, IBaseEntity
    {
        public string AvatarUrl { get; set; }

        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        public List<UserPrivateChat> UserPrivateChats { get; set; } = new List<UserPrivateChat>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
