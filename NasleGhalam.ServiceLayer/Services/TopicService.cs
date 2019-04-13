using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
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
        /// گرفتن  مبحث با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicViewModel GetById(int id)
        {
            return _topics
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه مبحث ها
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAllByLessonId(int id)
        {
            return _topics
                .Where(current => current.LessonId == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(TopicCreateViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            var hasRoot = _topics.Any(x => x.LessonId == topicViewModel.LessonId);

            if (hasRoot)
                return new ClientMessageResult
                {
                    Message = "برای این درس مبحث ریشه ثبت شده است!(تنها یک مبحث ریشه برای هر درس قابل ثبت است.)"
                };

            if (topicViewModel.ParentTopicId == null)
            {
                topic.ParentTopic = null;
                topic.ParentTopicId = null;
            }

            _topics.Add(topic);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(topic.Id);
            return clientResult;
        }

        /// <summary>
        /// ویرایش مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(TopicUpdateViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsChanged(topic);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(topic.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف مبحث
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var topicViewModel = GetById(id);
            if (topicViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsDeleted(topic);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }
    }
}
