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
                .Include(current => current.Topics)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationBookViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        ///  گرفتن همه کتاب های درسی یک درس
        /// </summary>
        /// <returns></returns>
        public IList<EducationBookViewModel> GetAllByLessonId(int lessonId)
        {
            return _educationBooks
                .Where(current => current.LessonId == lessonId)
                .AsEnumerable()
                .Select(Mapper.Map<EducationBookViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت کتاب درسی
        /// </summary>
        /// <param name="educationBookViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(EducationBookCreateViewModel educationBookViewModel)
        {
            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);

            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                educationBook.Topics.Add(topic);
            }
            _educationBooks.Add(educationBook);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationBook.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش کتاب درسی
        /// </summary>
        /// <param name="educationBookViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(EducationBookCreateViewModel educationBookViewModel)
        {
            var transaction = _uow.BeginTransaction();
            _uow.ExecuteSqlCommand("delete from Topics_EducationBooks where EducationBookId=@id",
                new SqlParameter("@id", educationBookViewModel.Id));

            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);
            _uow.MarkAsChanged(educationBook);

            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
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
            return Mapper.Map<MessageResultClient>(result);
        }


        /// <summary>
        /// حذف کتاب درسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var educationBook = _educationBooks
                .Include(current => current.Topics)
                .FirstOrDefault(current => current.Id == id);

            if (educationBook == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var topics = educationBook.Topics.ToList();
            foreach (var topic in topics)
            {
                educationBook.Topics.Remove(topic);
            }
            _uow.MarkAsDeleted(educationBook);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
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
