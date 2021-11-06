using System;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<AssayAnswerSheetViewModel, AssayAnswerSheet>().ForMember(dst => dst.Answers, opt => opt.MapFrom(src => string.Join(";", src.Answers)))
                .ReverseMap()
                .ForMember(dst => dst.Answers, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Answers) ? new List<string>() :  src.Answers.Split(';').ToList())); ;
            CreateMap<AssayAnswerSheetUpdateViewModel, AssayAnswerSheet>();
        }
    }
}
