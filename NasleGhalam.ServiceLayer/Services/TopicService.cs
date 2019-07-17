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
        /// گرفتن همه مبحث ها با آی دی درس
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
        /// گرفتن همه مبحث ها
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAll()
        {
            return _topics
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

            if (topicViewModel.ParentTopicId == 0 ||
                topicViewModel.ParentTopicId == null)
            {
                topic.ParentTopic = null;
                topic.ParentTopicId = null;
            }

            if (hasRoot && topic.ParentTopicId == null)
                return new ClientMessageResult
                {
                    Message = "برای این درس مبحث ریشه ثبت شده است!(تنها یک مبحث ریشه برای هر درس قابل ثبت است.)"
                };

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



        /// <summary>
        /// کپی مبجث های یک درس به درس دیگر
        /// </summary>

        public ClientMessageResult CopyTopicsToLesson(int LessonIdSource, int LessonIdTarget)
        {
            var sourceTopic = _topics.Where(x => x.LessonId == LessonIdSource).AsNoTracking().OrderBy(x => x.ParentTopicId).ToList();
            if (sourceTopic == null)
            {
                return ClientMessageResult.NotFound();
            }

            var topic = sourceTopic.Where(x => x.ParentTopicId == null).First();

            if (topic.ParentTopicId == null)
            {
                topic.LessonId = LessonIdTarget;
                Topic root = _topics.Add(topic);
                var childTopics = sourceTopic.Where(x => x.ParentTopicId == root.Id);
                foreach (Topic childTopic in childTopics)
                {
                    childTopic.LessonId = LessonIdTarget;
                    root.ChildrenTopic.Add(childTopic);
                    var childTopics2 = sourceTopic.Where(x => x.ParentTopicId == childTopic.Id);
                    foreach (Topic childTopic2 in childTopics2)
                    {
                        childTopic2.LessonId = LessonIdTarget;
                        childTopic.ChildrenTopic.Add(childTopic2);
                        var childTopics3 = sourceTopic.Where(x => x.ParentTopicId == childTopic2.Id);
                        foreach (Topic childTopic3 in childTopics3)
                        {
                            childTopic3.LessonId = LessonIdTarget;
                            childTopic2.ChildrenTopic.Add(childTopic3);
                            var childTopics4 = sourceTopic.Where(x => x.ParentTopicId == childTopic3.Id);
                            foreach (Topic childTopic4 in childTopics4)
                            {
                                childTopic4.LessonId = LessonIdTarget;
                                childTopic3.ChildrenTopic.Add(childTopic4);
                                var childTopics5 = sourceTopic.Where(x => x.ParentTopicId == childTopic4.Id);
                                foreach (Topic childTopic5 in childTopics5)
                                {
                                    childTopic5.LessonId = LessonIdTarget;
                                    childTopic4.ChildrenTopic.Add(childTopic5);
                                    var childTopics6 = sourceTopic.Where(x => x.ParentTopicId == childTopic5.Id);
                                    foreach (Topic childTopic6 in childTopics6)
                                    {
                                        childTopic6.LessonId = LessonIdTarget;
                                        childTopic5.ChildrenTopic.Add(childTopic6);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }




    }
}
