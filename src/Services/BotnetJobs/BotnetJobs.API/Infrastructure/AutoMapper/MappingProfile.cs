using AutoMapper;
using BotnetJobs.API.Models.v1;
using BotnetJobs.Domain.Entities;

namespace BotnetJobs.API.Infrastructure.AutoMapper
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
