using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.EducationGroup;
using NasleGhalam.ViewModels.Role;
using NasleGhalam.ViewModels.User;

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
        
                config.CreateMap<RoleViewModel, Role>();
                config.CreateMap<UserViewModel, User>();


            });
        }
    }
}