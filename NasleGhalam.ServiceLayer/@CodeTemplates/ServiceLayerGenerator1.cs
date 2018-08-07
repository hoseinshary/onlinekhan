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
        /// گرفتن  مبحث با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicViewModel GetById(int id)
        {
            return _topics
                .Where(current => current.Id == id)
                .Select(current => new TopicViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه مبحث ها
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAll()
        {
            return _topics.Select(current => new TopicViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(TopicViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            _topics.Add(topic);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = topic.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش مبحث
        /// </summary>
        /// <param name="topicViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(TopicViewModel topicViewModel)
        {
            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsChanged(topic);

			
			return  _uow.CommitChanges(CrudType.Update, Title);
			
        }


		/// <summary>
        /// حذف مبحث
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  topicViewModel = GetById(id);
            if (topicViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsDeleted(topic);
            
			return  _uow.CommitChanges(CrudType.Delete, Title);
			
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
                label = current.Name
            }).ToList();
        }
	}
}
