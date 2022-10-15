using Chat.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Chat.Infrastructure.Entities
{
    public class PrivateChat : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LastMessageId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<UserPrivateChat> UserPrivateChats { get; set; } = new List<UserPrivateChat>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
