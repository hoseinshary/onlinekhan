using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationYear;

namespace NasleGhalam.ServiceLayer.Services
{
	public class EducationYearService
	{
		private const string Title = "شهر";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationYear> _EducationYears;
       
	    public EducationYearService(IUnitOfWork uow)
        {
            _uow = uow;
            _EducationYears = uow.Set<EducationYear>();
        }


		/// <summary>
        /// گرفتن  شهر با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationYearViewModel GetById(int id)
        {
            return _EducationYears
                .Where(current => current.Id == id)
                .Select(current => new EducationYearViewModel
                {
                    Id = current.Id,
                    
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه شهر ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationYearViewModel> GetAll()
        {
            return _EducationYears.Select(current => new EducationYearViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت شهر
        /// </summary>
        /// <param name="EducationYearViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationYearViewModel EducationYearViewModel)
        {
            var EducationYear = Mapper.Map<EducationYear>(EducationYearViewModel);
            _EducationYears.Add(EducationYear);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = EducationYear.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش شهر
        /// </summary>
        /// <param name="EducationYearViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationYearViewModel EducationYearViewModel)
        {
            var EducationYear = Mapper.Map<EducationYear>(EducationYearViewModel);
            _uow.MarkAsChanged(EducationYear);

			
			return  _uow.CommitChanges(CrudType.Update, Title);
			
        }


		/// <summary>
        /// حذف شهر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  EducationYearViewModel = GetById(id);
            if (EducationYearViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var EducationYear = Mapper.Map<EducationYear>(EducationYearViewModel);
            _uow.MarkAsDeleted(EducationYear);
            
			return  _uow.CommitChanges(CrudType.Delete, Title);
			
        }


        /// <summary>
        /// گرفتن همه شهر ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _EducationYears.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
