using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.QuestionAnswer;

namespace NasleGhalam.ServiceLayer.Services
{
	public class QuestionAnswerService
	{
		private const string Title = "جواب سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionAnswer> _questionAnswers;
       
	    public QuestionAnswerService(IUnitOfWork uow)
        {
            _uow = uow;
            _questionAnswers = uow.Set<QuestionAnswer>();
        }


		/// <summary>
        /// گرفتن  جواب سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionAnswerViewModel GetById(int id)
        {
            return _questionAnswers
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerViewModel>)
                .FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه جواب سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionAnswerViewModel> GetAll()
        {
            return _questionAnswers
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerViewModel>)
                .ToList();
        }


		/// <summary>
        /// ثبت جواب سوال
        /// </summary>
        /// <param name="questionAnswerViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionAnswerCreateViewModel questionAnswerViewModel)
        {
            var questionAnswer = Mapper.Map<QuestionAnswer>(questionAnswerViewModel);
            _questionAnswers.Add(questionAnswer);

			var msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = questionAnswer.Id;
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


		/// <summary>
        /// ویرایش جواب سوال
        /// </summary>
        /// <param name="questionAnswerViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionAnswerUpdateViewModel questionAnswerViewModel)
        {
            var questionAnswer = Mapper.Map<QuestionAnswer>(questionAnswerViewModel);
            _uow.MarkAsChanged(questionAnswer);
			
			var msgRes = _uow.CommitChanges(CrudType.Update, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }


		/// <summary>
        /// حذف جواب سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  questionAnswerViewModel = GetById(id);
            if (questionAnswerViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var questionAnswer = Mapper.Map<QuestionAnswer>(questionAnswerViewModel);
            _uow.MarkAsDeleted(questionAnswer);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// گرفتن همه جواب سوال ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _questionAnswers.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
