using Chat.Common.DTOs.UserDTOs;
using Chat.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Chat.Core.Interfaces.Services
{
    public interface IAuthentificationService
    {
        Task RegistrationAsync(User user, string password);

        Task<UserAutorizationDTO> LoginAsync(LoginDTO logDTOs);
    }
}
