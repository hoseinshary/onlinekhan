using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.StudentMajorlist;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class StudentMajorlistProfile : Profile
    {
        public StudentMajorlistProfile()
        {
            CreateMap<StudentMajorlistViewModel, StudentMajorlist>();
            CreateMap<StudentMajorlistGetStudentViewModel, StudentMajorlist>();
            CreateMap<StudentMajorlistGetViewModel, StudentMajorlist>();
            CreateMap<StudentMajorlistUpdateViewModel, StudentMajorlist>();
        }
    }
}
