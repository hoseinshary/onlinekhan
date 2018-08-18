using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.EducationGroup_Lesson;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;
        //private readonly Services.EducationGroup_LessonService _educationGroupLessonService;
        



        public LessonService(IUnitOfWork uow , Services.EducationGroup_LessonService educationGroupLessonService)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
          //  _educationGroupLessonService = educationGroupLessonService;
        }


        /// <summary>
        /// گرفتن  درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonViewModel GetById(int id)
        {
            return _lessons
                .Where(current => current.Id == id)
                .Select(current => new LessonViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    IsMain = current.IsMain
                }).FirstOrDefault();
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
        public MessageResult Create(LessonViewModel lessonViewModel)
        {
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _lessons.Add(lesson);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = lesson.Id;
            return msgRes;
        }

        /// <summary>
        /// ثبت درس به همراه ضریب 
        /// </summary>
        /// <param name="LessonCreateAndUpdateViewModel"></param>
        /// <returns></returns>
        public MessageResult CreateLessonWithRatio(LessonCreateAndUpdateViewModel lessonCreateViewModel)
        {
            var currentLesson = _lessons.FirstOrDefault(current => current.Name == lessonCreateViewModel.Name);
            if (currentLesson != null)
            {
                MessageResult msg = new MessageResult();
                msg.FaMessage = "این درس قبلا ثبت شده است. برای تغییر از منوی ویرایش استفاده کنید";
                msg.MessageType = MessageType.Error;
                return msg;
            }

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
                        Rate = ratio.Ratio,
                        EducationSubGroupId = ratio.EducationSubGroupId
                    });
                }
            }

            _lessons.Add(l);
            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
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
        public MessageResult Update(LessonCreateAndUpdateViewModel lessonCreateViewModel)
        {

            ////خواندن اطلاعات واسط 
            //var previousEducationGroupLesson = _educationGroup_Lessons
            //    .Where(current => current.LessonId == educationGroup_LessonViewModel.First().LessonId).ToList();

            ////create 
            //foreach (EducationGroup_LessonViewModel egl in educationGroup_LessonViewModel)
            //{
            //    if (egl.IsChecked && !Utility.isExistInArray<int>(previousEducationGroupLesson.Select(x => x.EducationGroupId), egl.EducationGroupId))
            //    {
            //        var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(egl);
            //        _educationGroup_Lessons.Add(educationGroup_Lesson);
            //    }
            //}


            ////delete
            //foreach (EducationGroup_Lesson egl in previousEducationGroupLesson)
            //{
            //    if (!Utility.isExistInArray<int>(educationGroup_LessonViewModel.Where(x => x.IsChecked).Select(x => x.EducationGroupId), egl.EducationGroupId))
            //    {
            //        _uow.MarkAsDeleted(egl);
            //    }
            //}




            //msgRes = _uow.CommitChanges(CrudType.None, Title);
            //return msgRes;

            var lesson = Mapper.Map<Lesson>(lessonCreateViewModel);
            _uow.MarkAsChanged(lesson);

            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var lessonViewModel = GetById(id);
            if (lessonViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _uow.MarkAsDeleted(lesson);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه درس ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _lessons.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
