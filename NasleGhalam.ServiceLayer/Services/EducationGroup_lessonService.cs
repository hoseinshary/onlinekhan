using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationGroup_Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationGroup_LessonService
    {

        private const string Title = "رابط گروه آموزشی درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationGroup_Lesson> _educationGroup_Lessons;
        private readonly IDbSet<EducationGroup> _educationGroup;
        private readonly IDbSet<Lesson> _lessons;

        public EducationGroup_LessonService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationGroup_Lessons = uow.Set<EducationGroup_Lesson>();
            _educationGroup = uow.Set<EducationGroup>();
            _lessons = uow.Set<Lesson>();
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
                    Id = current.Id,
                    EducatioGroupName = current.EducationGroup.Name,
                    EducationGroupId = current.EducationGroupId,
                    LessonId = current.LessonId,
                    LessonName = current.Lesson.Name,
                    IsChecked = true

                }).FirstOrDefault();
     
        }


        /// <summary>
        /// گرفتن تمام درس ها با آی دی گروه آموزشی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<EducationGroup_LessonViewModel> GetAllLessonByEducationGroupId(int id)
        {
            return _educationGroup_Lessons
                .Where(current => current.EducationGroupId == id)
                .Select(current => new EducationGroup_LessonViewModel
                {
                    Id = current.Id,             
                    LessonName = current.Lesson.Name,
              
                }).ToList();
        }


        /// <summary>
        /// گرفتن همه گروه های آموزشی با آی دی درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<EducationGroup_LessonViewModel> GetAllEducationGroupByLessonId(int id)
        {    
            return _educationGroup
                
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
                EducatioGroupName = current.EducationGroup.Name,
                EducationGroupId = current.EducationGroupId,
                LessonId = current.LessonId,
                LessonName = current.Lesson.Name
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

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
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
            var educationGroup_LessonViewModel = GetById(id);
            if (educationGroup_LessonViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(educationGroup_LessonViewModel);
            _uow.MarkAsDeleted(educationGroup_Lesson);
            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// انتصاب درس به گروه آموزشی
        /// </summary>
        /// <param name="educationGroup_LessonViewModel"></param>
        /// <returns></returns>
        public MessageResult Change(IList<EducationGroup_LessonViewModel> educationGroup_LessonViewModel)
        {
            MessageResult msgRes;

            // educationGroup_LessonViewModel.GroupBy(current => current.LessonId)
              //  .Select(current => current.Key).ToList();

            //بررسی یکی بودن تمام آی دی درس ها
            for (int i = 0; i < educationGroup_LessonViewModel.Count; i++)
            {
                if(educationGroup_LessonViewModel[i].LessonId != educationGroup_LessonViewModel[i+1].LessonId)
                {
                    msgRes = new MessageResult();
                    msgRes.MessageType = MessageType.Error;
                    msgRes.FaMessage = "خطا در یکی نبودن آی دی درس ها ";
                    return msgRes;
                }
            }

            //خواندن اطلاعات واسط 
            var previousEducationGroupLesson = _educationGroup_Lessons
                .Where(current => current.LessonId == educationGroup_LessonViewModel.First().LessonId).ToList();

            //create 
            foreach (EducationGroup_LessonViewModel egl in educationGroup_LessonViewModel)
            {
                if( egl.IsChecked && !Utility.isExistInArray<int>(previousEducationGroupLesson.Select(x => x.EducationGroupId),egl.EducationGroupId))
                {
                    var educationGroup_Lesson = Mapper.Map<EducationGroup_Lesson>(egl);
                    _educationGroup_Lessons.Add(educationGroup_Lesson);
                }
            }


            //delete
            foreach (EducationGroup_Lesson egl in previousEducationGroupLesson)
            {
                if (!Utility.isExistInArray<int>(educationGroup_LessonViewModel.Where(x => x.IsChecked).Select(x=>x.EducationGroupId), egl.EducationGroupId))
                {
                    _uow.MarkAsDeleted(egl);
                }
            }


            

            msgRes = _uow.CommitChanges(CrudType.None, Title);
            return msgRes;
        }



    }
}
