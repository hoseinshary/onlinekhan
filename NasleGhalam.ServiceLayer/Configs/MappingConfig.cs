using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.Configs
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<LessonViewModel, Lesson>();
                //config.CreateMap<AreaViewModel, Area>();
                //config.CreateMap<SensorViewModel, Sensor>();
                //config.CreateMap<RoleViewModel, Role>();
                //config.CreateMap<UserViewModel, User>();
            });
        }
    }
}