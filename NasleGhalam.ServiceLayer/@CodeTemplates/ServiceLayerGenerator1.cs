﻿using System;
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
                .Where(current => current.Id == id)
                .Select(current => new QuestionViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAll()
        {
            return _questions.Select(current => new QuestionViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(QuestionViewModel questionViewModel)
        {
            var question = Mapper.Map<Question>(questionViewModel);
            _questions.Add(question);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = question.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(QuestionViewModel questionViewModel)
        {
            var question = Mapper.Map<Question>(questionViewModel);
            _uow.MarkAsChanged(question);

			
			return  _uow.CommitChanges(CrudType.Update, Title);
			
        }


		/// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  questionViewModel = GetById(id);
            if (questionViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var question = Mapper.Map<Question>(questionViewModel);
            _uow.MarkAsDeleted(question);
            
			return  _uow.CommitChanges(CrudType.Delete, Title);
			
        }


        /// <summary>
        /// گرفتن همه سوال ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _questions.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
