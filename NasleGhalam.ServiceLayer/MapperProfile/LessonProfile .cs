using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<LessonViewModel, Lesson>();
            CreateMap<LessonCreateViewModel, Lesson>();
            CreateMap<LessonUpdateViewModel, Lesson>();
        }
    }
}
