using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.QuestionGroup;

namespace NasleGhalam.ServiceLayer.Services
{
	public class QuestionGroupService
	{
		private const string Title = "سوال گروهی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionGroup> _questionGroups;
       
	    public QuestionGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _questionGroups = uow.Set<QuestionGroup>();
        }


		/// <summary>
        /// گرفتن  سوال گروهی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionGroupViewModel GetById(int id)
        {
            return _questionGroups
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionGroupViewModel>)
                .FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه سوال گروهی ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionGroupViewModel> GetAll()
        {
            return _questionGroups
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionGroupViewModel>)
                .ToList();
        }


		/// <summary>
        /// ثبت سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(QuestionGroupCreateViewModel questionGroupViewModel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _questionGroups.Add(questionGroup);

			var msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = questionGroup.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


		/// <summary>
        /// ویرایش سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _uow.MarkAsChanged(questionGroup);
			
			var msgRes = _uow.CommitChanges(CrudType.Update, Title);
			return Mapper.Map<MessageResultClient>(msgRes);
        }


		/// <summary>
        /// حذف سوال گروهی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
			var  questionGroupViewModel = GetById(id);
            if (questionGroupViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _uow.MarkAsDeleted(questionGroup);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// گرفتن همه سوال گروهی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _questionGroups.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
