using Chat.Common.DTOs.MessageDTOs;
using Chat.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        [HttpPost("message")]
        public async Task<IActionResult> CreateGroup(MessageCreateDTO messageDTO)
        {
            await _messageService.CreateMessage(messageDTO, UserId);

            return Ok();
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessageListAsync(int chatId, int chatType)
        {
            var messages = await _messageService.GetMessagesAsync(UserId, chatId, chatType);
            
            return Ok(messages);
        }

    }
}
