using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicCreateViewModel, Topic>();
            CreateMap<TopicViewModel, Topic>();
        }
    }
}
