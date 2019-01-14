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
        public MessageResultClient Create(TopicCreateViewModel topicViewModel)
        {
            var hasRoot = _topics.Any(x => x.LessonId == topicViewModel.LessonId);
            if (!hasRoot)
            {
                var topic = Mapper.Map<Topic>(topicViewModel);

                topic.ParentTopic = null;
                topic.ParentTopicId = null;

                _topics.Add(topic);

                MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
                msgRes.Id = topic.Id;
                return Mapper.Map<MessageResultClient>(msgRes);
            }
            else if (topicViewModel.ParentTopicId != null )
            {
                var topic = Mapper.Map<Topic>(topicViewModel);
                _topics.Add(topic);

                MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
                msgRes.Id = topic.Id;
                return Mapper.Map<MessageResultClient>(msgRes);
            }
            else
            {
                MessageResultServer msgRes = new MessageResultServer();
                msgRes.FaMessage = "برای این درس مبحث ریشه ثبت شده است!(تنها یک مبحث ریشه برای هر درس قابل ثبت است.)";
                return Mapper.Map<MessageResultClient>(msgRes);
            }
        }


        /// <summary>
        /// ویرایش مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(TopicUpdateViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsChanged(topic);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف مبحث
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var topicViewModel = GetById(id);
            if (topicViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsDeleted(topic);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


    }
}
