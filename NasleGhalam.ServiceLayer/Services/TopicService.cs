using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.ServiceLayer.Services
{
    public class TopicService
    {
        private const string Title = "مبحث";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Topic> _topics;

        public TopicService(IUnitOfWork uow)
        {
            _uow = uow;
            _topics = uow.Set<Topic>();
            
        }


        /// <summary>
        /// گرفتن  تمام مبحث ها مربوط به یک گروه آموزشی درس به صورت درختی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicGetNameViewModel GetAllTree(int educationGroup_LessonId)
        {
            return _topics
                .Where(current => current.EducationGroup_LessonId == educationGroup_LessonId )
                .Select(current => new TopicGetNameViewModel
                {
                    Id = current.Id,
                    Title = current.Title,
                    ParentTopicId = current.ParentTopicId,


                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن  تمام مبحث های ریشه مربوط به یک گروه آموزشی درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicGetNameViewModel GetAllRoot(int educationGroup_LessonId)
        {
            return _topics
                .Where(current => current.EducationGroup_LessonId == educationGroup_LessonId && current.ParentTopicId == null)
                .Select(current => new TopicGetNameViewModel
                {
                    Id = current.Id,
                    Title = current.Title,
                    ParentTopicId = current.ParentTopicId,
                    

                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن  مبحث های فرزند یک مبحث مربوط به یک گروه آموزشی درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicGetNameViewModel GetAllChild(int educationGroup_LessonId , int parentTopicId)
        {
            return _topics
                .Where(current => current.ParentTopicId == parentTopicId && current.EducationGroup_LessonId == educationGroup_LessonId)
                .Select(current => new TopicGetNameViewModel
                {
                    Id = current.Id,
                    Title = current.Title,
                    ParentTopicId = current.ParentTopicId

                }).FirstOrDefault();
        }



        /// <summary>
        /// گرفتن  مبحث با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicGetViewModel GetById(int id)
        {
            return _topics
                .Where(current => current.Id == id)
                .Select(current => new TopicGetViewModel
                {
                    Id = current.Id,
                    Title = current.Title,
                   // AreaType = current.AreaType,
                    EducationGroup_LessonId = current.EducationGroup_LessonId,
                    ExamStock = current.ExamStock,
                    ExamStockSystem = current.ExamStockSystem,
                   // HardnessType = current.HardnessType,
                    Importance = current.Importance,
                    IsActive = current.IsActive,
                    IsExamSource = current.IsExamSource,
                    ParentTopicId = current.ParentTopicId,
                    

                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه مبحث های مربوط به یک گروه آموزشی درس
        /// </summary>
        /// <returns></returns>
        public IList<TopicGetViewModel> GetAllByEducationGroup_LessonId(int educationGroup_LessonId)
        {
            return _topics.Where(current => current.EducationGroup_LessonId == educationGroup_LessonId).Select(current => new TopicGetViewModel()
            {
                Id = current.Id,
                Title = current.Title,
                //AreaType = current.AreaType,
                EducationGroup_LessonId = current.EducationGroup_LessonId,
                ExamStock = current.ExamStock,
                ExamStockSystem = current.ExamStockSystem,
                //HardnessType = current.HardnessType,
                Importance = current.Importance,
                IsActive = current.IsActive,
                IsExamSource = current.IsExamSource,
            }).ToList();
        }


        /// <summary>
        /// ثبت مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(TopicCreateViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            _topics.Add(topic);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = topic.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(TopicCreateViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsChanged(topic);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف مبحث
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var topicViewModel = GetById(id);
            if (topicViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsDeleted(topic);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه مبحث ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _topics.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Title
            }).ToList();
        }
    }
}
