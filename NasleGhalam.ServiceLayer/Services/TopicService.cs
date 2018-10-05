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

        private IList<Topic> _topicsByEducationGroup_Lesson;

        public TopicService(IUnitOfWork uow)
        {
            _uow = uow;
            _topics = uow.Set<Topic>();

        }


        public TopicTreeViewModel children(int id)
        {

            if (_topicsByEducationGroup_Lesson.All(x => x.ParentTopicId != id))
            {
                return _topicsByEducationGroup_Lesson.Where(x => x.Id == id).Select(current => new TopicTreeViewModel
                {
                    Id = current.Id,
                    label = current.Title,
                }).FirstOrDefault();
            }
            else
            {
                var returnvalue = new TopicTreeViewModel();
                var currentTopic = _topicsByEducationGroup_Lesson.FirstOrDefault(x => x.Id == id);
                returnvalue.Id = currentTopic.Id;
                returnvalue.label = currentTopic.Title;
                returnvalue.children = new List<TopicTreeViewModel>();
                foreach (var item in _topicsByEducationGroup_Lesson.Where(x => x.ParentTopicId == id).ToList())
                {
                    returnvalue.children.Add(children(item.Id));
                }
                return returnvalue;
            }
        }

        /// <summary>
        /// گرفتن  تمام مبحث ها مربوط به یک گروه آموزشی درس به صورت درختی
        /// </summary>
        /// <param name="educationGroup_LessonId"></param>
        /// <returns></returns>
        public IEnumerable<TopicTreeViewModel> GetAllTree(int id)
        {

            _topicsByEducationGroup_Lesson = _topics.Where(x => x.EducationGroup_LessonId == id).ToList();

            var topic = _topicsByEducationGroup_Lesson
                .FirstOrDefault(x => x.ParentTopicId == null);

            var result = new List<TopicTreeViewModel>();
            if (topic != null)
            {
                result.Add(children(topic.Id));
            }
            return result;
        }


        /// <summary>
        /// گرفتن  تمام مبحث های ریشه مربوط به یک گروه آموزشی درس
        /// </summary>
        /// <param name="educationGroup_LessonId"></param>
        /// <returns></returns>
        public IList<TopicGetNameViewModel> GetAllRoot(int educationGroup_LessonId)
        {
            return _topics
                .Where(current => current.EducationGroup_LessonId == educationGroup_LessonId && current.ParentTopicId == null)
                .Select(current => new TopicGetNameViewModel
                {
                    Id = current.Id,
                    Title = current.Title,
                    ParentTopicId = current.ParentTopicId,


                }).ToList();
        }


        /// <summary>
        /// گرفتن  مبحث های فرزند یک مبحث مربوط به یک گروه آموزشی درس
        /// </summary>
        /// <param name="educationGroup_LessonId"></param>
        /// <param name="parentTopicId"></param>
        /// <returns></returns>
        public IList<TopicGetNameViewModel> GetAllChild(int educationGroup_LessonId, int parentTopicId)
        {
            return _topics
                .Where(current => current.ParentTopicId == parentTopicId && current.EducationGroup_LessonId == educationGroup_LessonId)
                .Select(current => new TopicGetNameViewModel
                {
                    Id = current.Id,
                    Title = current.Title,
                    ParentTopicId = current.ParentTopicId

                }).ToList();
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
                    LookupId_AreaType = current.LookupId_AreaType,
                    EducationGroup_LessonId = current.EducationGroup_LessonId,
                    ExamStock = current.ExamStock,
                    ExamStockSystem = current.ExamStockSystem,
                    LookupId_HardnessType = current.LookupId_HardnessType,
                    Importance = current.Importance,
                    IsActive = current.IsActive,
                    IsExamSource = current.IsExamSource,
                    ParentTopicId = current.ParentTopicId,


                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه مبحث های مربوط به یک گروه آموزشی درس
        /// </summary>
        /// <param name="educationGroup_LessonId"></param>
        /// <returns></returns>
        public IList<TopicGetViewModel> GetAllByEducationGroup_LessonId(int educationGroup_LessonId)
        {
            return _topics.Where(current => current.EducationGroup_LessonId == educationGroup_LessonId).Select(current => new TopicGetViewModel()
            {
                Id = current.Id,
                Title = current.Title,
                LookupId_AreaType = current.LookupId_AreaType,
                EducationGroup_LessonId = current.EducationGroup_LessonId,
                ExamStock = current.ExamStock,
                ExamStockSystem = current.ExamStockSystem,
                LookupId_HardnessType = current.LookupId_HardnessType,
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
            if (topicViewModel.ParentTopicId != null)
            {
                var topic = Mapper.Map<Topic>(topicViewModel);
                _topics.Add(topic);

                MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
                msgRes.Id = topic.Id;
                return msgRes;
            }
            else if (topicViewModel.ParentTopicId == null && !_topics.Any(x => x.EducationGroup_LessonId == topicViewModel.EducationGroup_LessonId))
            {
                var topic = Mapper.Map<Topic>(topicViewModel);
                _topics.Add(topic);

                MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
                msgRes.Id = topic.Id;
                return msgRes;
            }
            else
            {
                MessageResult msgRes = new MessageResult();
                msgRes.FaMessage = "برای این درس مبحث ریشه ثبت شده است!(تنها یک مبحث ریشه برای هر درس قابل ثبت است.)";
                return msgRes;
            }
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
