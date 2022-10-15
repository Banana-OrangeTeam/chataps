using AutoMapper;
using Chat.Common.DTOs.MessageDTOs;
using Chat.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Helpers
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Message, MessageInfoDTO>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.SenderId, act => act.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.Text, act => act.MapFrom(src => src.Text))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(src => src.CreatedAt));
        }


    }
}
