
using Chat.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Chat.Infrastructure.Entities
{
    public class UserGroup: IBaseEntity
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }


    }
}
