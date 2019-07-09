using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Assay;
using NasleGhalam.ViewModels.Assey;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class AssayProfile : Profile
    {
        public AssayProfile()
        {

            CreateMap<QuestionAssayViewModel, Question>();
            CreateMap<LessonAssayViewModel, Lesson>();
            CreateMap<AssayViewModel, Assay>();
            CreateMap<AssayCreateViewModel, Assay>();

        }
    }
}
