using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationGroupWithSubGroups;

namespace NasleGhalam.ServiceLayer.Services
{
	public class EducationGroupWithSubGroupsService
	{
		private const string Title = "مبحث";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationGroupWithSubGroups> _educationGroupWithSubGroupss;
       
	    public EducationGroupWithSubGroupsService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationGroupWithSubGroupss = uow.Set<EducationGroupWithSubGroups>();
        }


		/// <summary>
        /// گرفتن  مبحث با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationGroupWithSubGroupsViewModel GetById(int id)
        {
            return _educationGroupWithSubGroupss
                .Where(current => current.Id == id)
                .Select(current => new EducationGroupWithSubGroupsViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه مبحث ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationGroupWithSubGroupsViewModel> GetAll()
        {
            return _educationGroupWithSubGroupss.Select(current => new EducationGroupWithSubGroupsViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت مبحث
        /// </summary>
        /// <param name="educationGroupWithSubGroupsViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationGroupWithSubGroupsViewModel educationGroupWithSubGroupsViewModel)
        {
            var educationGroupWithSubGroups = Mapper.Map<EducationGroupWithSubGroups>(educationGroupWithSubGroupsViewModel);
            _educationGroupWithSubGroupss.Add(educationGroupWithSubGroups);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = educationGroupWithSubGroups.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش مبحث
        /// </summary>
        /// <param name="educationGroupWithSubGroupsViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationGroupWithSubGroupsViewModel educationGroupWithSubGroupsViewModel)
        {
            var educationGroupWithSubGroups = Mapper.Map<EducationGroupWithSubGroups>(educationGroupWithSubGroupsViewModel);
            _uow.MarkAsChanged(educationGroupWithSubGroups);

			
			return  _uow.CommitChanges(CrudType.Update, Title);
			
        }


		/// <summary>
        /// حذف مبحث
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  educationGroupWithSubGroupsViewModel = GetById(id);
            if (educationGroupWithSubGroupsViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var educationGroupWithSubGroups = Mapper.Map<EducationGroupWithSubGroups>(educationGroupWithSubGroupsViewModel);
            _uow.MarkAsDeleted(educationGroupWithSubGroups);
            
			return  _uow.CommitChanges(CrudType.Delete, Title);
			
        }


        /// <summary>
        /// گرفتن همه مبحث ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationGroupWithSubGroupss.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
