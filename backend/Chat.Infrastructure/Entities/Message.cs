using Chat.Infrastructure.Interfaces;
using System;

namespace Chat.Infrastructure.Entities
{
    public class Message : IBaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string SenderId { get; set; }
        public User Sender { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public int? PrivateChatId { get; set; }
        public PrivateChat? PrivateChat { get; set; }
    }
}
