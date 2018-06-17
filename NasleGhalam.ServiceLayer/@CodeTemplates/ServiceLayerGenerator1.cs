using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationGroup_Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
	public class EducationGroup_LessonService
	{
		private const string Title = "رابط گروه آموزشی درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationGroup_Lesson> _educationGroup_Lessons;
       
	    public EducationGroup_LessonService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationGroup_Lessons = uow.Set<EducationGroup_Lesson>();
        }


		/// <summary>
        /// گرفتن  رابط گروه آموزشی درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationGroup_LessonViewModel GetById(int id)
        {
            return _educationGroup_Lessons
                .Where(current => current.Id == id)
                .Select(current => new EducationGroup_LessonViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه رابط گروه آموزشی درس ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationGroup_LessonViewModel> GetAll()
        {
            return _educationGroup_Lessons.Select(current => new EducationGroup_LessonViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت رابط گروه آموزشی درس
        /// </summary>
        /// <param name="educationGroup_LessonViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationGroup_LessonViewModel educationGroup_LessonViewModel)
        {
            var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(educationGroup_LessonViewModel);
            _educationGroup_Lessons.Add(educationGroup_Lesson);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = educationGroup_Lesson.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش رابط گروه آموزشی درس
        /// </summary>
        /// <param name="educationGroup_LessonViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationGroup_LessonViewModel educationGroup_LessonViewModel)
        {
            var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(educationGroup_LessonViewModel);
            _uow.MarkAsChanged(educationGroup_Lesson);
            return _uow.CommitChanges(CrudType.Update, Title);
        }


		/// <summary>
        /// حذف رابط گروه آموزشی درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  educationGroup_LessonViewModel = GetById(id);
            if (educationGroup_LessonViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(educationGroup_LessonViewModel);
            _uow.MarkAsDeleted(educationGroup_Lesson);
            return _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه رابط گروه آموزشی درس ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationGroup_Lessons.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
