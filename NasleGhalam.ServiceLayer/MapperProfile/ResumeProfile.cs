using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Resume;
using Newtonsoft.Json;


namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class ResumeProfile : Profile
    {
        public ResumeProfile()
        {
            CreateMap<ResumeViewModel, Resume>()
                .ForMember(dst => dst.TeachingResumeJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.TeachingResumes)))
                .ForMember(dst => dst.EducationCertificateJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.EducationCertificates)))
                .ForMember(dst => dst.PublicationJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Publications)))
                .ReverseMap()
                .ForMember(dst => dst.TeachingResumes,
                    opt => opt.MapFrom(src => JsonConvert.DeserializeObject<TeachingResumeViewModel>(src.TeachingResumeJson)))
                .ForMember(dst => dst.EducationCertificates,
                    opt => opt.MapFrom(src => JsonConvert.DeserializeObject<EducationCertificateViewModel>(src.EducationCertificateJson)))
                .ForMember(dst => dst.Publications,
                    opt => opt.MapFrom(src => JsonConvert.DeserializeObject<PublicationViewModel>(src.PublicationJson)));
        }
    }
}
