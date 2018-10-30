using AutoMapper;
using NasleGhalam.ServiceLayer.MapperProfile;

namespace NasleGhalam.ServiceLayer.Configs
{
    public static class SiteConfig
    {
        public static void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(RoleProfile).Assembly));
        }
    }
}
