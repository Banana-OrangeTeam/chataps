using System;

namespace Chat.Common.DTOs.MessageDTOs
{
    public class MessageInfoDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
