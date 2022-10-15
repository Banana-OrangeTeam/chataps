using Chat.Common.DTOs.MessageDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core.Interfaces.Services
{
    public interface IMessageService
    {
        Task CreateMessage(MessageCreateDTO messageDTO, string userId);
        Task<List<MessageInfoDTO>> GetMessagesAsync(string userId, int chatId, int chatType);
    }
}
