using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationBook;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationBookService
    {
        private const string Title = "کتاب درسی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationBook> _educationBooks;

        public EducationBookService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationBooks = uow.Set<EducationBook>();
        }


        /// <summary>
        /// گرفتن  کتاب درسی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationBookViewModel GetById(int id)
        {
            return _educationBooks
                .Where(current => current.Id == id)
                .Select(current => new EducationBookViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    IsActive = current.IsActive,
                    IsChanged = current.IsChanged,
                    IsExamSource = current.IsExamSource,
                    PublishYear = current.PublishYear,
                    EducationGroupId = current.EducationGroup_Lesson.EducationGroupId,
                    EducationGroup_LessonId = current.EducationGroup_LessonId,
                    GradeLevelId = current.GradeLevelId,
                    TopicIds = current.Topics.Select(topic => topic.Id)
                }).FirstOrDefault();
        }


        /// <summary>
        ///  گرفتن همه کتاب های درسی یک پایه ی تحصیلی
        /// </summary>
        /// <returns></returns>
        public IList<EducationBookViewModel> GetAllByGradeLevelId(int gradeLevelId)
        {
            return _educationBooks
                .Where(current => current.GradeLevelId == gradeLevelId)
                .Select(current => new EducationBookViewModel()
                {
                    Id = current.Id,
                    Name = current.Name,
                    IsActive = current.IsActive,
                    IsChanged = current.IsChanged,
                    IsExamSource = current.IsExamSource,
                    PublishYear = current.PublishYear,
                    LessonName = current.EducationGroup_Lesson.Lesson.Name
                }).ToList();
        }


        /// <summary>
        /// ثبت کتاب درسی
        /// </summary>
        /// <param name="educationBookViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationBookViewModel educationBookViewModel)
        {
            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);
            var topics = _uow.Set<Topic>();

            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic()
                {
                    Id = topicId
                };
                topics.Attach(topic);
                educationBook.Topics.Add(topic);
            }
            _educationBooks.Add(educationBook);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationBook.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش کتاب درسی
        /// </summary>
        /// <param name="educationBookViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationBookViewModel educationBookViewModel)
        {
            var transaction = _uow.BeginTransaction();
            _uow.ExecuteSqlCommand("delete from Topics_EducationBooks where EducationBookId=@id", 
                new SqlParameter("@id", educationBookViewModel.Id));

            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);
            _uow.MarkAsChanged(educationBook);
            var topics = _uow.Set<Topic>();

            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic()
                {
                    Id = topicId
                };
                topics.Attach(topic);
                educationBook.Topics.Add(topic);
            }

            var result = _uow.CommitChanges(CrudType.Update, Title);
            if (result.MessageType == MessageType.Success)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
            return result;
        }


        /// <summary>
        /// حذف کتاب درسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var educationBookViewModel = GetById(id);
            if (educationBookViewModel == null)
            {
                return Utility.NotFoundMessage();
            }
            var topics = _uow.Set<Topic>();
            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                topics.Attach(topic);
                _uow.MarkAsDeleted(topic);
            }

            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);
            _uow.MarkAsDeleted(educationBook);

            return _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه کتاب درسی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationBooks.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
