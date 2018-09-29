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
using  NasleGhalam.ViewModels.AxillaryBook;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.UniversityBranch;

namespace NasleGhalam.ServiceLayer.Configs
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<LessonViewModel, Lesson>();
                config.CreateMap<ViewModels.EducationGroup.EducationGroupViewModel, EducationGroup>();
                config.CreateMap<EducationGroup_LessonViewModel, EducationGroup_Lesson>();
                config.CreateMap<EducationGroupLessonViewModel, EducationGroup_Lesson>();

                config.CreateMap<RoleViewModel, Role>();
                config.CreateMap<UserCreateViewModel, User>();
                config.CreateMap<UserUpdateViewModel, User>();
                config.CreateMap<UserGetViewModel, User>();
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
            });
        }
    }
}