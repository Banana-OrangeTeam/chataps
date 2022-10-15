using Chat.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Chat.Infrastructure.Entities
{
    public class Group : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
        public int LastMessageId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<UserGroup> UserGroups { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
