using AutoMapper;
using Bots.API.Models.v1;
using Bots.Domain.Entities;

namespace Bots.API.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBotModel, Bot>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateBotModel, Bot>();
        }
    }
}
