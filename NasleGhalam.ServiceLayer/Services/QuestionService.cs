using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionService
    {
        private const string Title = "سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Question> _questions;

        public QuestionService(IUnitOfWork uow)
        {
            _uow = uow;
            _questions = uow.Set<Question>();
        }


        /// <summary>
        /// گرفتن  سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionViewModel GetById(int id)
        {
            return _questions
                .Include(current => current.QuestionOptions)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه سوال های مباحث
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByTopicIds(IEnumerable<int> ids)
        {
           
            return _questions
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(QuestionCreateViewModel questionViewModel)
        {
            var question = Mapper.Map<Question>(questionViewModel);
            _questions.Add(question);

            foreach (var topicId in questionViewModel.TopicsId)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }

            foreach (var tagId in questionViewModel.TagsId)
            {
                var tag = new Tag() { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            foreach (var option in questionViewModel.Options)
            {
                var newOption = new QuestionOption()
                {
                    Context = option.Context,
                    IsAnswer = option.IsAnswer,
                };

                question.QuestionOptions.Add(newOption);
            }




            _uow.ValidateOnSaveEnabled(false);
            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = question.Id;
            var resutlVal = Mapper.Map<MessageResultClient>(msgRes);
            resutlVal.Obj = new
            {
                question.Context,
                question.FileName
            };
            return resutlVal;
        }


        /// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(QuestionUpdateViewModel questionViewModel)
        {

            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags  )
                .First(current => current.Id == questionViewModel.Id);

            question.AuthorName = questionViewModel.AuthorName;
            question.Description = questionViewModel.Description;
            question.InsertDateTime = DateTime.Now;
            question.IsActive = questionViewModel.IsActive;
            question.IsStandard = questionViewModel.IsStandard;
            question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionHardnessType = questionViewModel.LookupId_QuestionHardnessType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.LookupId_RepeatnessType = questionViewModel.LookupId_RepeatnessType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.QuestionPoint = questionViewModel.QuestionPoint;
            question.ResponseSecond = questionViewModel.ResponseSecond;
            question.UseEvaluation = questionViewModel.UseEvaluation;
            






            //delete topics
            var deleteTopicList = question.Topics
                .Where(oldTopic => questionViewModel.TopicsId.All(newTopicId => newTopicId != oldTopic.Id))
                .ToList();
            foreach (var topic in deleteTopicList)
            {
                question.Topics.Remove(topic);
            }

            //add topics
            var addTopicList = questionViewModel.TopicsId
                .Where(oldTopicId => question.Topics.All(newTopic => newTopic.Id != oldTopicId))
                .ToList();
            foreach (var topicId in addTopicList)
            {
                var topic = new Topic { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }



            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagsId.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagsId
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            

            
            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
            
        }


        /// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var question = _questions
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .First(current => current.Id == id);

            if (question == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            //remove topics
            var topics = question.Topics.ToList();
            foreach (var item in topics)
            {
                question.Topics.Remove(item);

            }

            //remove tags
            var tags = question.Tags.ToList();
            foreach (var item in tags)
            {
                _uow.MarkAsDeleted(item);
            }


            //remove options
            var options = question.QuestionOptions.ToList();
            foreach (var item in options)
            {
                _uow.MarkAsDeleted(item);
            }


            _uow.MarkAsDeleted(question);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }



    }
}
