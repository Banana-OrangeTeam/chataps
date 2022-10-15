using Chat.Common.DTOs.ChatDTOs;
using Chat.Common.DTOs.UserDTOs;
using Chat.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Core.Interfaces.Services
{
    public interface IChatService
    {
        Task CreateGroup(GroupCreateDTO groupCreateDTO, string userId);
        Task CreatePrivateChat(PrivateChatCreateDTO privateChatCreateDTO, string userId);
        Task<List<ChatInfoDTO>> GetAllChats(string userId);
        Task<List<UserInfoDTO>> GetAllChatMembersAsync(int chatId, int chatType, string userId);
        Task AddChatMembersAsync(AddChatMemberDTO addChatMemberDTO, string userId);
    }
}
