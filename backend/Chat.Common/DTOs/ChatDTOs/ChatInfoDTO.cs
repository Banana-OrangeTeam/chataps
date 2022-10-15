using System;

namespace Chat.Common.DTOs.ChatDTOs
{
    public class ChatInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatTypeId { get; set; }
        public string LastMessageText { get; set; }
        public DateTime LastMessageCreatedAt { get; set; }
    }
}
