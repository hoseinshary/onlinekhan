using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;
        private readonly IDbSet<Ratio> _ratios;
        private readonly IDbSet<EducationGroup> _educationGroups;
        private readonly IDbSet<EducationGroup_Lesson> _educationGroup_Lesson;

        private readonly EducationGroup_LessonService _educationGroupLessonService;
        private readonly RatioService _ratioService;

        //private readonly TopicService _topicService;



        
        public LessonService(IUnitOfWork uow , EducationGroup_LessonService educationGroupLessonService, RatioService ratioService/*, TopicService topicService*/)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
            _educationGroupLessonService = educationGroupLessonService;
            _ratioService = ratioService;
            //_topicService = topicService;
        
            _ratios = uow.Set<Ratio>();
            _educationGroups = uow.Set<EducationGroup>();
            _educationGroup_Lesson = uow.Set<EducationGroup_Lesson>();
        }


        /// <summary>
        /// گرفتن  درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonCreateAndUpdateViewModel GetById(int id)
        {
            
            return _lessons
                .Where(less => less.Id == id)
                .Select(less => new LessonCreateAndUpdateViewModel
                {
                    Name = less.Name,
                    IsMain = less.IsMain,
                    Id = less.Id,
                    GradeLevelId = less.GradeLevelId,
                    LookupId_Nezam = less.LookupId_Nezam,
                    EducationGroups = 
                    _educationGroups
                    .Where(x=> x.EducationSubGroups.Any())
                    .Select(edg => new EducationGroupLessonViewModel
                    {
                        IsChecked = edg.EducationGroups_Lessons.Any(edl => edl.LessonId == id),
                        EducationGroupId = edg.Id,
                        EducationGroupName = edg.Name,
                        SubGroups = edg.EducationSubGroups.Select(eds => new RatioLessonViewModel
                        {
                            Id = eds.Ratios.Any(x => x.LessonId == id) ? eds.Ratios.FirstOrDefault(x=>  x.LessonId == id).Id : 0,
                            Rate = eds.Ratios.Any(x =>  x.LessonId == id) ? eds.Ratios.FirstOrDefault(x => x.LessonId == id).Rate : (byte)0,
                            EducationSubGroupId = eds.Id,
                            EducationSubGroupName = eds.Name
                        })
                    })
                }).DefaultIfEmpty().FirstOrDefault() ;

            //var x = _educationGroup_Lessons.Where(edul => edul.LessonId == id)
            //    .SelectMany(edul => _educationGroups.Where(edug => edug.Id == edul.EducationGroupId).DefaultIfEmpty(new { }),
            //    (edul, edug) => new
            //    {
            //        IsChecked = true,
            //        edug.Id,
            //        edug.Name,
            //        groups = edug.EducationSubGroups.Select(edusg => new
            //        {
            //            edusg.Id,
            //            edusg.Ratios.FirstOrDefault(ra => ra.LessonId == id).Rate,
            //            RatioId = edusg.Ratios.FirstOrDefault(ra => ra.LessonId == id).Id,
            //            edusg.Name
            //        })
            //    }).ToList();
            //var lesson = _lessons.Where(current => current.Id == id).Select(x => new {
            //    x.Id,
            //    x.Name,
            //    x.IsMain,
            //    EducationGroups_Lessons = _educationGroups.Where(ed => ed.Id == x.)
            //        x.EducationGroups_Lessons.Select(y => new {
            //            y.EducationGroupId,
            //            y.EducationGroup.Name,
            //            IsChecked = y.EducationGroup
            //            }).SelectMany(c => _educationGroups.Where(ed => ed.Id == c.EducationGroupId).DefaultIfEmpty(),
            //            (edu, less) => new EducationGroup_LessonViewModel
            //            {
            //                Id = edu.,
            //                EducatioGroupName = edu.Name,
            //                EducationGroupId = edu.EducationGroupId,
            //                LessonId = edu == null ? 0 : less.Id,
            //                LessonName = edu_lesson.Lesson.Name,
            //                IsChecked = edu_lesson != null
            //            }
            //            ) }).FirstOrDefault();
            //returnVal.Id = lesson.Id;
            //returnVal.Name = lesson.Name;
            //returnVal.IsMain = lesson.IsMain;



            //var educationGroups = _educationGroups

            //     .Select(current => new
            //     {
            //         current.Id,
            //         current.Name,

            //         EducationGroups_Lessons = current.EducationGroups_Lessons
            //         .Where(x => x.LessonId == id).DefaultIfEmpty()
            //     })
            //     .SelectMany(curent => curent.EducationGroups_Lessons,
            //     (education, edu_lesson) => new EducationGroup_LessonViewModel
            //     {
            //         Id = edu_lesson.Id,
            //         EducatioGroupName = education.Name,
            //         EducationGroupId = education.Id,
            //         LessonId = edu_lesson == null ? 0 : edu_lesson.LessonId,
            //         LessonName = edu_lesson.Lesson.Name,
            //         IsChecked = edu_lesson != null
            //     }).OrderByDescending(current => current.IsChecked).ToList();

            //foreach (var item in educationGroups)
            //{
            //    List<RatioLessonViewModel> subGroups = new List<RatioLessonViewModel>();
            //    if (item.LessonId != 0)
            //    {
            //        subGroups = _ratios
            //           .Where(current => current.EducationSubGroup.EducationGroupId == item.EducationGroupId)
            //           .Select(x => new RatioLessonViewModel
            //           {
            //               Id = x.Id,
            //               EducationSubGroupId = x.Id,
            //               EducationSubGroupName = x.EducationSubGroup.Name,
            //               Ratio = x.Rate

            //           }).ToList();
            //    }

            //    returnVal.EducationGroups.Add(new EducationGroupLessonViewModel()
            //    {
            //        EducationGroupId = item.EducationGroupId,
            //        IsChecked = item.LessonId == 0 ? false : true,
            //        EducationGroupName = item.EducatioGroupName,
            //        SubGroups = subGroups.Count == 0 ? null : subGroups
            //    });

            //}
            
        }


        /// <summary>
        /// گرفتن همه درس ها
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAll()
        {
            return _lessons.Select(current => new LessonViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                IsMain = current.IsMain
            }).OrderByDescending(m => m.IsMain).ToList();
        }


        /// <summary>
        /// ثبت درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Create(LessonViewModel lessonViewModel)
        {
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _lessons.Add(lesson);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = lesson.Id;
            return msgRes;
        }

        /// <summary>
        /// ثبت درس به همراه ضریب 
        /// </summary>
        /// <param name="LessonCreateAndUpdateViewModel"></param>
        /// <returns></returns>
        public MessageResultServer CreateLessonWithRatio(LessonCreateAndUpdateViewModel lessonCreateViewModel)
        {
            //var currentLesson = _lessons.FirstOrDefault(current => current.Name == lessonCreateViewModel.Name.Replace(" ", ""));
            //if (currentLesson != null)
            //{
            //    return new MessageResult
            //    {
            //        FaMessage = "این درس قبلا ثبت شده است. برای تغییر از منوی ویرایش استفاده کنید",
            //        MessageType = MessageType.Error
            //    };
            //}

            Lesson l = new Lesson()
            {
                Name = lessonCreateViewModel.Name,
                IsMain = lessonCreateViewModel.IsMain
            };

            foreach (var educationGroup in lessonCreateViewModel.EducationGroups)
            {
                l.EducationGroups_Lessons.Add(new EducationGroup_Lesson
                {
                    EducationGroupId = educationGroup.EducationGroupId
                });

                foreach (var ratio in educationGroup.SubGroups)
                {
                    l.Ratios.Add(new Ratio()
                    {
                        Rate = ratio.Rate,
                        EducationSubGroupId = ratio.EducationSubGroupId
                    });
                }
            }

            _lessons.Add(l);
            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = l.Id;
            return msgRes;


            //_lessons.
            //MessageResult msgResEducationGroupLesson = new MessageResult();
            //MessageResult msgResRatio = new MessageResult();
            //MessageResult msgResLesson = new MessageResult();

            ////first:create lesson

            //var lessonViewModel = new LessonViewModel()
            //{
            //    Id = lessonCreateViewModel.Id,
            //    Name = lessonCreateViewModel.Name,
            //    IsMain = lessonCreateViewModel.IsMain
            //};

            //var transaction = _uow.BeginTransaction();
            //msgResLesson = Create(lessonViewModel);


            ////second and third

            //var crLessEdu = new EducationGroup_LessonService(_uow);
            //var crRatio = new RatioService(_uow);

            //foreach (var item in lessonCreateViewModel.EducationGroups)
            //{
            //    //second:create EducationGroup_LessonService
            //    var educationGroupLesson = new EducationGroup_LessonViewModel()
            //    {
            //        EducationGroupId = item.EducationGroupId,
            //        LessonId = msgResLesson.Id
            //    };

            //    msgResEducationGroupLesson = crLessEdu.Create(educationGroupLesson);

            //    //third create ratio
            //    foreach (var VARIABLE in item.SubGroups)
            //    {
            //        var ratio = new ViewModels.Ratio.RatioViewModel()
            //        {
            //            Rate = VARIABLE.Ratio,
            //            EducationSubGroupId = VARIABLE.EducationSubGroupId,
            //            LessonId = msgResLesson.Id

            //        };

            //      msgResRatio = crRatio.Create(ratio);

            //    }

            //}

            //if (msgResLesson.MessageType == MessageType.Success && msgResEducationGroupLesson.MessageType == MessageType.Success && msgResRatio.MessageType == MessageType.Success)
            //{
            //    transaction.Commit();
            //    return msgResLesson;
            //}
            //else if (msgResLesson.MessageType != MessageType.Success)
            //{
            //    return msgResLesson;
            //}
            //else if (msgResEducationGroupLesson.MessageType != MessageType.Success)
            //{
            //    return msgResEducationGroupLesson;
            //}
            //else
            //{
            //    return msgResRatio;
            //}

        }


        /// <summary>
        /// ویرایش درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Update(LessonCreateAndUpdateViewModel lessonCreateViewModel)
        {

            //خواندن اطلاعات واسط 
            var previousEducationGroupLesson = _educationGroup_Lesson
                .Where(current => current.LessonId == lessonCreateViewModel.Id).ToList();

            //create 
            var allRatios = _ratios.AsNoTracking().ToList();
            foreach (var eg in lessonCreateViewModel.EducationGroups)
            {
                if ( eg.IsChecked && !Utility.isExistInArray<int>(previousEducationGroupLesson.Select(x => x.EducationGroupId), eg.EducationGroupId))
                {
                    var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(eg);
                    educationGroup_Lesson.LessonId = lessonCreateViewModel.Id;
                    _educationGroup_Lesson.Add(educationGroup_Lesson);
                    foreach (var esg in eg.SubGroups)
                    {
                        var ratio = Mapper.Map<Ratio>(esg);
                        ratio.LessonId = lessonCreateViewModel.Id;
                        _ratios.Add(ratio);
                    }
                }
                else if(eg.IsChecked)
                {
           
                    foreach (var esg in eg.SubGroups)
                    {
                        var ratio = Mapper.Map<Ratio>(esg);
                        if (allRatios.Any(x => x.Id == ratio.Id))
                        {
                            _ratios.Attach(ratio);
                            _uow.UpdateFields(ratio, x => x.Rate);
                        }
                        else
                        {
                            ratio.LessonId = lessonCreateViewModel.Id;
                            _ratios.Add(ratio);
                        }
                    }
                }
            }


            //delete
            foreach (EducationGroup_Lesson egl in previousEducationGroupLesson)
            {
                if ( !Utility.isExistInArray<int>(lessonCreateViewModel.EducationGroups.Select(x => x.EducationGroupId), egl.EducationGroupId))
                {
                    _uow.MarkAsDeleted(egl);
                    foreach (var item in lessonCreateViewModel.EducationGroups.Where(c => c.EducationGroupId == egl.EducationGroupId).First().SubGroups)
                    {
                        var ratio = Mapper.Map<Ratio>(item);
                        _uow.MarkAsDeleted(ratio);
                    }
                }
            }

            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultServer Delete(int id)
        {
            var lesson = _lessons
                .Where(current => current.Id == id)
                .Select(current => new 
                {
                    HasTopic = current.EducationGroups_Lessons.Any(edu_Lesson => edu_Lesson.Topics.Any()),
                    Ratios = current.Ratios,
                    EducationGroups_Lessons = current.EducationGroups_Lessons
                }).FirstOrDefault();

            if (lesson == null)
            {
                return Utility.NotFoundMessage();
            }
            else if (lesson.HasTopic)
            {
                return new MessageResultServer
                {
                    FaMessage = "برای این درس قبلا عنوان تعریف شده است.درس مورد نظر حذف نگردید",
                    MessageType = MessageType.Error
                };

            }

            var ratios = lesson.Ratios;
            foreach (var ratio in ratios)
            {
                _uow.MarkAsDeleted(ratio);
            }

            var eduLesson = lesson.EducationGroups_Lessons;
            foreach (var educationGroupsLesson in eduLesson)
            {
                _uow.MarkAsDeleted(educationGroupsLesson);
            }

            Lesson entityLesson = new Lesson()
            {
                Id = id
            };

            _uow.MarkAsDeleted(entityLesson);


            return _uow.CommitChanges(CrudType.Delete, Title);

        }

        /// <summary>
        /// گرفتن همه درس ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<LessonDdlViewModel> GetAllDdl() // todo: چرا در سرویس گروه آموزشس_درس نوشته نشده؟
        {
            return _educationGroup_Lesson.Select(current => new LessonDdlViewModel
            {
                value = current.Id,
                label = current.Lesson.Name,
                educationGroupId = current.EducationGroupId
            }).ToList();
        }
    }
}
