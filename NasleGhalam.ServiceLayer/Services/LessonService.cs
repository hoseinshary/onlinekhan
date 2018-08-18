﻿using System;
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
using NasleGhalam.ViewModels.EducationSubGroup;
using NasleGhalam.ViewModels.EducationGroup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;
        private readonly IDbSet<EducationGroup_Lesson> _educationGroup_Lessons;
        private readonly IDbSet<Ratio> _ratios;
        private readonly IDbSet<EducationGroup> _educationGroups;



        public LessonService(IUnitOfWork uow)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
            _educationGroup_Lessons = uow.Set<EducationGroup_Lesson>();
            _ratios = uow.Set<Ratio>();
        }


        /// <summary>
        /// گرفتن  درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonCreateAndUpdateViewModel GetById(int id)
        {
            LessonCreateAndUpdateViewModel returnVal = new LessonCreateAndUpdateViewModel();
            var lesson = _lessons.Where(current => current.Id == id).FirstOrDefault();
            returnVal.Id = lesson.Id;
            returnVal.Name = lesson.Name;
            returnVal.IsMain = lesson.IsMain;


            
            var educationGroups = _educationGroups

                 .Select(current => new
                 {
                     current.Id,
                     current.Name,

                     EducationGroups_Lessons = current.EducationGroups_Lessons
                     .Where(x => x.LessonId == id).DefaultIfEmpty()
                 })
                 .SelectMany(curent => curent.EducationGroups_Lessons,
                 (education, edu_lesson) => new EducationGroup_LessonViewModel
                 {
                     Id = edu_lesson.Id,
                     EducatioGroupName = education.Name,
                     EducationGroupId = education.Id,
                     LessonId = edu_lesson == null ? 0 : edu_lesson.LessonId,
                     LessonName = edu_lesson.Lesson.Name,
                     IsChecked = edu_lesson != null
                 }).OrderByDescending(current => current.IsChecked).ToList();

            foreach (var item in educationGroups)
            {
                List<RatioLessonViewModel> subGroups = new List<RatioLessonViewModel>();
                if (item.LessonId != 0)
                {
                     subGroups = _ratios
                        .Where(current => current.EducationSubGroup.EducationGroupId == item.EducationGroupId)
                        .Select(x => new RatioLessonViewModel
                        {
                            Id = x.Id,
                            EducationSubGroupId = x.Id,
                            EducationSubGroupName = x.EducationSubGroup.Name,
                            Ratio = x.Rate

                        }).ToList();
                }
                
                returnVal.EducationGroups.Add(new EducationGroupLessonViewModel()
                {
                    EducationGroupId = item.EducationGroupId,
                    IsChecked = item.LessonId == 0 ? false : true,
                    EducationGroupName = item.EducatioGroupName,
                    SubGroups =subGroups.Count == 0 ? null : subGroups
                });

            }
            return returnVal;
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
            var currentLesson = _lessons.FirstOrDefault(current => current.Name == lessonCreateViewModel.Name.Replace(" ", ""));
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

            //خواندن اطلاعات واسط 
            var previousEducationGroupLesson = _educationGroup_Lessons
                .Where(current => current.LessonId == lessonCreateViewModel.Id).ToList();

            //create 
            foreach (var eg in lessonCreateViewModel.EducationGroups)
            {
                if (!Utility.isExistInArray<int>(previousEducationGroupLesson.Select(x => x.EducationGroupId), eg.EducationGroupId))
                {
                    var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(eg);
                    _educationGroup_Lessons.Add(educationGroup_Lesson);
                    foreach (var esg in eg.SubGroups)
                    {
                        var ratio = Mapper.Map<Ratio>(esg);
                        ratio.LessonId = lessonCreateViewModel.Id;
                        _ratios.Add(ratio);
                    }
                }
            }


            //delete
            foreach (EducationGroup_Lesson egl in previousEducationGroupLesson)
            {
                if (!Utility.isExistInArray<int>(lessonCreateViewModel.EducationGroups.Select(x => x.EducationGroupId), egl.EducationGroupId))
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
