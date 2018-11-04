using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class RatioProfile : Profile
    {
        public RatioProfile()
        {
            CreateMap<RatioViewModel, Ratio>();
            CreateMap<RatioCreateViewModel, Ratio>();
            CreateMap<RatioUpdateViewModel, Ratio>();
        }
    }
}
