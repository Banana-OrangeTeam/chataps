namespace Chat.Common.DTOs.MessageDTOs
{
    public class MessageCreateDTO
    {
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int ChatType { get; set; }
    }
}
