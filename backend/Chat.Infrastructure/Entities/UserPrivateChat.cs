
using Chat.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Chat.Infrastructure.Entities
{
    public class UserPrivateChat : IBaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int PrivateChatId { get; set; }
        public PrivateChat PrivateChat { get; set; }
    }
}
