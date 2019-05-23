using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.LessonDepartment;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class LessonDepartmentProfile : Profile
    {
        public LessonDepartmentProfile()
        {
            CreateMap<LessonDepartment, LessonDepartment>();
            CreateMap<LessonDepartmentAssignViewModel, LessonDepartment>();
            
        }
    }
}
