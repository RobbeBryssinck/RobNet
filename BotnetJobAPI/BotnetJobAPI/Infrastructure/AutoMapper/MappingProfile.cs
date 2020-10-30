using AutoMapper;
using BotnetJobAPI.Domain.Entities;
using BotnetJobAPI.Models.v1;

namespace BotnetJobAPI.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBotnetJobModel, BotnetJob>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateBotnetJobModel, BotnetJob>();
        }
    }
}
