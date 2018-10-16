using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.Role;
using NasleGhalam.ViewModels.User;
using NasleGhalam.ViewModels.Grade;
using NasleGhalam.ViewModels.GradeLevel;
using NasleGhalam.ViewModels.EducationGroup_Lesson;
using NasleGhalam.ViewModels.Province;
using NasleGhalam.ViewModels.City;
using NasleGhalam.ViewModels.EducationBook;
using NasleGhalam.ViewModels.EducationYear;
using NasleGhalam.ViewModels.Exam;
using NasleGhalam.ViewModels.EducationSubGroup;
using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.Ratio;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.ViewModels.Publisher;
using NasleGhalam.ViewModels.AxillaryBook;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.UniversityBranch;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Student;

namespace NasleGhalam.ServiceLayer.Configs
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<LessonViewModel, Lesson>();
                config.CreateMap<LessonCreateAndUpdateViewModel, Lesson>();
                config.CreateMap<ViewModels.EducationGroup.EducationGroupViewModel, EducationGroup>();
                config.CreateMap<EducationGroup_LessonViewModel, EducationGroup_Lesson>();
                config.CreateMap<EducationGroupLessonViewModel, EducationGroup_Lesson>();

                config.CreateMap<RoleViewModel, Role>();

                config.CreateMap<UserCreateViewModel, User>();
                config.CreateMap<UserUpdateViewModel, User>();
                config.CreateMap<UserViewModel, User>()
                    .ReverseMap()
                    .ForMember(dst => dst.GenderName, opt => opt.MapFrom(src => src.Gender ? "پسر" : "دختر"))
                    .ForMember(dst => dst.ProvinceId, opt => opt.MapFrom(src => src.City.ProvinceId));

                config.CreateMap<StudentCreateViewModel, Student>();
                config.CreateMap<StudentUpdateViewModel, Student>();
                config.CreateMap<StudentViewModel, Student>()
                    .ReverseMap()
                    .ForPath(dst => dst.User.ProvinceId, opt => opt.MapFrom(src => src.User.City.ProvinceId))
                    .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User));

                config.CreateMap<GradeViewModel, Grade>();
                config.CreateMap<GradeLevelViewModel, GradeLevel>();

                config.CreateMap<ProvinceViewModel, Province>();

                config.CreateMap<CityViewModel, City>();

                config.CreateMap<EducationYearViewModel, EducationYear>();

                config.CreateMap<ExamViewModel, Exam>();

                config.CreateMap<EducationSubGroupViewModel, EducationSubGroup>();
                config.CreateMap<RatioViewModel, Ratio>();
                config.CreateMap<RatioLessonViewModel, Ratio>();

                config.CreateMap<LookupViewModel, Lookup>();

                config.CreateMap<TopicCreateViewModel, Topic>();
                config.CreateMap<TopicGetViewModel, Topic>();

                config.CreateMap<PublisherViewModel, Publisher>();
                config.CreateMap<AxillaryBookViewModel, AxillaryBook>();

                config.CreateMap<EducationBookViewModel, EducationBook>();

                config.CreateMap<TagViewModel, Tag>();

                config.CreateMap<QuestionCreateViewModel, Question>();

                config.CreateMap<UniversityBranchViewModel, UniversityBranch>();

                config.CreateMap<MessageResultServer, MessageResultClient>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.FaMessage))
                .ForMember(dest => dest.Obj, opt => opt.Ignore());

            });
        }
    }
}