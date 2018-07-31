using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Module;

namespace NasleGhalam.ServiceLayer.Services
{
	public class ModuleService
	{
		private const string Title = "ماژول";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Module> _modules;
       
	    public ModuleService(IUnitOfWork uow)
        {
            _uow = uow;
            _modules = uow.Set<Module>();
        }


		/// <summary>
        /// گرفتن  ماژول با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModuleViewModel GetById(int id)
        {
            return _modules
                .Where(current => current.Id == id)
                .Select(current => new ModuleViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه ماژول ها
        /// </summary>
        /// <returns></returns>
        public IList<ModuleViewModel> GetAll()
        {
            return _modules.Select(current => new ModuleViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت ماژول
        /// </summary>
        /// <param name="moduleViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(ModuleViewModel moduleViewModel)
        {
            var module = Mapper.Map<Module>(moduleViewModel);
            _modules.Add(module);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = module.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش ماژول
        /// </summary>
        /// <param name="moduleViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(ModuleViewModel moduleViewModel)
        {
            var module = Mapper.Map<Module>(moduleViewModel);
            _uow.MarkAsChanged(module);

			
			return  _uow.CommitChanges(CrudType.Update, Title);
			
        }


		/// <summary>
        /// حذف ماژول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  moduleViewModel = GetById(id);
            if (moduleViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var module = Mapper.Map<Module>(moduleViewModel);
            _uow.MarkAsDeleted(module);
            
			return  _uow.CommitChanges(CrudType.Delete, Title);
			
        }


        /// <summary>
        /// گرفتن همه ماژول ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _modules.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
