using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.QuestionOption;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionCreateViewModel, Question>();
            CreateMap<QuestionOptionViewModel, QuestionOption>();
        }
    }
}
