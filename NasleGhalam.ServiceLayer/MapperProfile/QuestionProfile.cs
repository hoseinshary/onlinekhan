using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionCreateViewModel, Question>();
        }
    }
}
