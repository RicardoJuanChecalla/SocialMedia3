using System;
using AutoMapper;
using SocialMedia3.Core.Entities;
using SocialMedia3.Core.DTOs;

namespace SocialMedia3.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<Security, SecurityDto>().ReverseMap();
        }
    }
}