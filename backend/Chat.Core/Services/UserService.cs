using Chat.Core.Interfaces.Services;
using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Interfaces;

namespace Chat.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Group> _groupRepository;

        public UserService(
            IRepository<User> userRepository,
            IRepository<Group> groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

    }
}
