using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.EducationGroup;
using NasleGhalam.ViewModels.Role;
using NasleGhalam.ViewModels.User;
using NasleGhalam.ViewModels.Grade;
using NasleGhalam.ViewModels.GradeLevel;
using NasleGhalam.ViewModels.EducationGroup_Lesson;
using NasleGhalam.ViewModels.Province;
using NasleGhalam.ViewModels.City;
using NasleGhalam.ViewModels.EducationYear;

namespace NasleGhalam.ServiceLayer.Configs
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<LessonViewModel, Lesson>();
                config.CreateMap<EducationGroupViewModel, EducationGroup>();
                config.CreateMap<EducationGroup_LessonViewModel, EducationGroup_Lesson>();

                config.CreateMap<RoleViewModel, Role>();
                config.CreateMap<UserViewModel, User>();

                config.CreateMap<GradeViewModel, Grade>() ;
                config.CreateMap<GradeLevelViewModel, GradeLevel>();

                config.CreateMap<ProvinceViewModel, Province>();

                config.CreateMap<CityViewModel, City>();

                config.CreateMap<EducationYearViewModel, EducationYear>();

            });
        }
    }
}