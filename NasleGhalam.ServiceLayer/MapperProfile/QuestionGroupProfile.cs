using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.ViewModels.QuestionOption;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class QuestionGroupProfile : Profile
    {
        public QuestionGroupProfile()
        {
            CreateMap<QuestionGroupCreateViewModel, QuestionGroup>();
            CreateMap<QuestionGroupUpdateViewModel, QuestionGroup>();
            CreateMap<QuestionGroupViewModel, QuestionGroup>();
            
        }
    }
}
