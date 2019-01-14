using AutoMapper;
using NasleGhalam.Common;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class PublicProfile : Profile
    {
        public PublicProfile()
        {
            CreateMap<MessageResultServer, MessageResultClient>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.FaMessage))
                .ForMember(dest => dest.Obj, opt => opt.Ignore());
        }
    }
}
