using AutoMapper;
using Chat.Core.Exceptions;
using Chat.Core.Interfaces.Services;
using Chat.Infrastructure.Entities;
using Chat.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Core.ChatTypesEnum;
using System;
using Chat.Common.DTOs.ChatDTOs;
using Chat.Common.DTOs.UserDTOs;

namespace Chat.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserGroup> _userGroupRepository;
        private readonly IRepository<UserPrivateChat> _userPrivateChatRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<PrivateChat> _privateChatRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ChatService(
            UserManager<User> userManager,
            IRepository<User> userRepository,
            IRepository<UserGroup> userGroupRepository,
            IRepository<UserPrivateChat> userPrivateChatRepository,
            IRepository<Group> groupRepository,
            IRepository<PrivateChat> privateChatRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
            _userPrivateChatRepository = userPrivateChatRepository;
            _groupRepository = groupRepository;
            _privateChatRepository = privateChatRepository;
            _mapper = mapper;
        }
        public async Task CreateGroup(GroupCreateDTO groupCreateDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "User not found");

            var group = new Group()
            {
                Name = groupCreateDTO.Name,
                Description = groupCreateDTO.Description,
                CreatorId = userId
            };

            await _groupRepository.AddAsync(group);
            await _groupRepository.SaveChangesAsync();

            var userGroup = new UserGroup()
            {
                GroupId = group.Id,
                UserId = userId
            };

            await _userGroupRepository.AddAsync(userGroup);
            await _userGroupRepository.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task CreatePrivateChat(PrivateChatCreateDTO privateChatCreateDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var toUser = await _userManager.FindByEmailAsync(privateChatCreateDTO.ToUserEmail);
            if (user == null || toUser == null)
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "User not found");

            var chatExist = await _userPrivateChatRepository
                .Query()
                .Where(x => x.UserId == userId || x.UserId == toUser.Id)
                .GroupBy(x => x.PrivateChatId)
                .AnyAsync(x => x.Count() > 1);

            if (chatExist)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Chat already exist");

            var privateChat = new PrivateChat()
            {
                Name = privateChatCreateDTO.Name,
                UserPrivateChats = new List<UserPrivateChat>()
                {
                    new UserPrivateChat()
                    {
                        UserId = user.Id
                    },

                    new UserPrivateChat()
                    {
                        UserId = toUser.Id
                    },
                }
            };

            await _privateChatRepository.AddAsync(privateChat);
            await _privateChatRepository.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task<List<ChatInfoDTO>> GetAllChats(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var groups = await _userGroupRepository.Query()
                .Where(x => x.UserId == userId)
                .Include(x => x.Group)
                    .ThenInclude(x => x.Messages)
                    .Select(x => new ChatInfoDTO()
                    {
                        Id = x.GroupId,
                        Name = x.Group.Name,
                        ChatTypeId = (int)ChatTypes.GroupChat,
                        LastMessageText = (x.Group.Messages.Count > 0) ?
                            x.Group.Messages
                                .OrderByDescending(y => y.CreatedAt)
                                .First()
                                .Text
                                : "",

                        LastMessageCreatedAt = (x.Group.Messages.Count > 0)
                            ? x.Group.Messages.
                            OrderByDescending(y => y.CreatedAt)
                            .First()
                            .CreatedAt :
                            x.Group.CreatedAt
                    })
                .ToListAsync();

            var privateChats = await _userPrivateChatRepository.Query()
                .Where(x => x.UserId == userId)
                .Include(x => x.PrivateChat)
                    .ThenInclude(x => x.Messages)
                    .Select(x => new ChatInfoDTO()
                    {
                        Id = x.PrivateChatId,
                        Name = x.PrivateChat.Name,
                        ChatTypeId = (int)ChatTypes.PrivateChat,
                        LastMessageText = (x.PrivateChat.Messages.Count > 0) ?
                            x.PrivateChat.Messages
                                .OrderByDescending(y => y.CreatedAt)
                                .First()
                                .Text
                                : "",

                        LastMessageCreatedAt = (x.PrivateChat.Messages.Count > 0)
                            ? x.PrivateChat.Messages.
                            OrderByDescending(y => y.CreatedAt)
                            .First()
                            .CreatedAt :
                            x.PrivateChat.CreatedAt
                    })
                .ToListAsync();

            groups.AddRange(privateChats);

            return groups.OrderByDescending(x => x.LastMessageCreatedAt).ToList();
        }

        public async Task<List<UserInfoDTO>> GetAllChatMembersAsync(int chatId, int chatType, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "User not found");

            if (chatType == (int)ChatTypes.PrivateChat)
            {
                var chat = await _privateChatRepository.GetByKeyAsync(chatId) 
                    ?? throw new HttpException(System.Net.HttpStatusCode.NotFound, "chat not found");

                var isChatMember = chat.UserPrivateChats.Where(x => x.UserId == userId).FirstOrDefault()
                    ?? throw new HttpException(System.Net.HttpStatusCode.Forbidden, "You are not chat member");

                var chats = await _privateChatRepository.Query()
                    .Where(x => x.Id == chatId)
                    .Include(x => x.UserPrivateChats)
                        .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync();

                var members = chats.UserPrivateChats
                    .Select(x => new UserInfoDTO()
                    {
                        Id = x.UserId,
                        Email = x.User.Email,
                        UserName = x.User.UserName,
                    }).ToList();

                return members;
            }
            else
            {
                var chat = await _groupRepository.Query()
                    .Where(x => x.Id == chatId)
                    .Include(x => x.UserGroups)
                    .FirstOrDefaultAsync()
                    ?? throw new HttpException(System.Net.HttpStatusCode.NotFound, "chat not found");

                var isChatMember = chat.UserGroups.Where(x => x.UserId == userId).FirstOrDefault()
                    ?? throw new HttpException(System.Net.HttpStatusCode.Forbidden, "You are not chat member");

                var chats = await _groupRepository.Query()
                    .Where(x => x.Id == chatId)
                    .Include(x => x.UserGroups)
                        .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync();
                
                var members = chats.UserGroups
                    .Select(x => new UserInfoDTO()
                    {
                        Id = x.UserId,
                        Email = x.User.Email,
                        UserName = x.User.UserName,
                    }).ToList();

                return members;
            }
        }

        public async Task AddChatMembersAsync(AddChatMemberDTO addChatMemberDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var chat = _groupRepository.GetByKeyAsync(addChatMemberDTO.ChatId).Result;

            if (user == null || chat == null)
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "Not found");
            else if (userId != chat.CreatorId )
                throw new HttpException(System.Net.HttpStatusCode.Forbidden, "Only group creator can add user");

            foreach (var item in addChatMemberDTO.MembersEmailToAdd)
            {
                var groupMember = _userManager.FindByEmailAsync(item).Result;
                var userGroup = new UserGroup()
                {
                    UserId = userId,
                    User = groupMember,
                    GroupId = addChatMemberDTO.ChatId,
                    Group = chat
                };

                await _userGroupRepository.AddAsync(userGroup);
            }

            await _userGroupRepository.SaveChangesAsync();
        }
    }
}
