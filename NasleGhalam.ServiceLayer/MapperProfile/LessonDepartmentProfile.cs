using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.LessonDepartment;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class LessonDepartmentProfile : Profile
    {
        public LessonDepartmentProfile()
        {
            CreateMap<LessonDepartmentViewModel, LessonDepartment>();
            CreateMap<LessonDepartmentAssignViewModel, LessonDepartment>();
            
        }
    }
}
