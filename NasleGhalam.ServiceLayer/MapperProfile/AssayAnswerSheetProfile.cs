using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.AssayAnswerSheet;


namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class AssayAnswerSheetProfile : Profile
    {
        public AssayAnswerSheetProfile()
        {
            CreateMap<AssayAnswerSheetCreateViewModel, AssayAnswerSheet>();
            CreateMap<AssayAnswerSheetViewModel, AssayAnswerSheet>();
            CreateMap<AssayAnswerSheetUpdateViewModel, AssayAnswerSheet>();
        }
    }
}
