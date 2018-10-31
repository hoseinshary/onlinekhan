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
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios.Select(ratio => ratio.EducationSubGroup))
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه درس ها
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAll()
        {
            return _lessons
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(LessonCreateViewModel lessonViewModel)
        {
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _lessons.Add(lesson);

            var educationTrees = _uow.Set<EducationTree>();
            foreach (var TreeId in lessonViewModel.EducationTreeIds)
            {
                var tree = new EducationTree() {Id = TreeId};
                educationTrees.Attach(tree);
                lesson.EducationTrees.Add(tree);
            }

            //foreach (var ratio in lessonViewModel.Ratios)
            //{
            //    lesson.Ratios.Add(new Ratio()
            //    {
            //        Rate = ratio.Rate,
            //        EducationSubGroupId = ratio.EducationSubGroupId
            //    });
            //}


            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = lesson.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(LessonViewModel lessonViewModel)
        {
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _uow.MarkAsChanged(lesson);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var lessonViewModel = GetById(id);
            if (lessonViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _uow.MarkAsDeleted(lesson);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
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
