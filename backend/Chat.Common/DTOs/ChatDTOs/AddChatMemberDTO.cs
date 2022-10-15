namespace Chat.Common.DTOs.ChatDTOs
{
    public class AddChatMemberDTO
    {
        public int ChatId { get; set; }
        public string[] MembersEmailToAdd { get; set; }
    }
}
