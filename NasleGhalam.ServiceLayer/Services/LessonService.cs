using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;

        public LessonService(IUnitOfWork uow)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
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
            }).ToList();
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
        /// ویرایش درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(LessonViewModel lessonViewModel)
        {
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
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
