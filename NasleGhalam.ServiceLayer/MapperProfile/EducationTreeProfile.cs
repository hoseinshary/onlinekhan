﻿using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationTree;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class EducationTreeProfile : Profile
    {
        public EducationTreeProfile()
        {
            CreateMap<EducationTreeViewModel, EducationTree>();
            CreateMap<EducationTreeCreateViewModel, EducationTree>();
            CreateMap<EducationTreeUpdateViewModel, EducationTree>();
        }
    }
}
