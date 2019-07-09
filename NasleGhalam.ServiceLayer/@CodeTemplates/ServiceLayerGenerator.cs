using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Assay;

namespace NasleGhalam.ServiceLayer.Services
{
	public class AssayService
	{
		private const string Title = "آزمون";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Assay> _assays;
       
	    public AssayService(IUnitOfWork uow)
        {
            _uow = uow;
            _assays = uow.Set<Assay>();
        }

		/// <summary>
        /// گرفتن  آزمون با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssayViewModel GetById(int id)
        {
            return _assays
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayViewModel>)
                .FirstOrDefault();
        }

		/// <summary>
        /// گرفتن همه آزمون ها
        /// </summary>
        /// <returns></returns>
        public IList<AssayViewModel> GetAll()
        {
            return _assays
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayViewModel>)
                .ToList();
        }

		/// <summary>
        /// ثبت آزمون
        /// </summary>
        /// <param name="assayViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(AssayCreateViewModel assayViewModel)
        {
            var assay = Mapper.Map<Assay>(assayViewModel);
            _assays.Add(assay);

			var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assay.Id);

            return clientResult;
        }

		/// <summary>
        /// ویرایش آزمون
        /// </summary>
        /// <param name="assayViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(AssayUpdateViewModel assayViewModel)
        {
            var assay = Mapper.Map<Assay>(assayViewModel);
            _uow.MarkAsChanged(assay);
			
			 var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assay.Id);

            return clientResult;
        }

		/// <summary>
        /// حذف آزمون
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  assayViewModel = GetById(id);
            if (assayViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var assay = Mapper.Map<Assay>(assayViewModel);
            _uow.MarkAsDeleted(assay);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }
	}
}
