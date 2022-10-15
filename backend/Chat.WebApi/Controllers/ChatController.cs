using Chat.Common.DTOs.ChatDTOs;
using Chat.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(
            IChatService chatService)
        {
            _chatService = chatService;
        }
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        [HttpPost("group")]
        public async Task<IActionResult> CreateGroupAsync(GroupCreateDTO groupCreateDTO)
        {
            await _chatService.CreateGroup(groupCreateDTO, UserId);

            return Ok();
        }

        [HttpPost("privateChat")]
        public async Task<IActionResult> CreatePrivateChatAsync(PrivateChatCreateDTO privateChatCreateDTO)
        {
            await _chatService.CreatePrivateChat(privateChatCreateDTO, UserId);

            return Ok();
        }

        [Authorize]
        [HttpGet("chats")]
        public async Task<IActionResult> GetChatsAsync()
        {
            var chats = await _chatService.GetAllChats(UserId);

            return Ok(chats);
        }

        [HttpGet("members")]
        public async Task<IActionResult> GetAllChatMembersAsync(int chatId, int chatType)
        {
            var chatMembers = await _chatService.GetAllChatMembersAsync(chatId, chatType, UserId);

            return Ok(chatMembers);
        }

        [HttpPut("members")]
        public async Task<IActionResult> AddChatMembersAsync(AddChatMemberDTO addChatMemberDTO)
        {
            await _chatService.AddChatMembersAsync(addChatMemberDTO, UserId);
            return Ok();
        }
    }
}
