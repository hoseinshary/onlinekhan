using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.ProgramItem;

namespace NasleGhalam.ServiceLayer.Services
{
	public class ProgramItemService
	{
		private const string Title = "آیتم برنامه هفتگی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<ProgramItem> _programItems;
       
	    public ProgramItemService(IUnitOfWork uow)
        {
            _uow = uow;
            _programItems = uow.Set<ProgramItem>();
        }

		/// <summary>
        /// گرفتن  آیتم برنامه هفتگی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProgramItemViewModel GetById(int id)
        {
            return _programItems
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ProgramItemViewModel>)
                .FirstOrDefault();
        }

		/// <summary>
        /// گرفتن همه آیتم برنامه هفتگی ها
        /// </summary>
        /// <returns></returns>
        public IList<ProgramItemViewModel> GetAll()
        {
            return _programItems
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ProgramItemViewModel>)
                .ToList();
        }

		/// <summary>
        /// ثبت آیتم برنامه هفتگی
        /// </summary>
        /// <param name="programItemViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(ProgramItemCreateViewModel programItemViewModel)
        {
            var programItem = Mapper.Map<ProgramItem>(programItemViewModel);
            _programItems.Add(programItem);

			var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(programItem.Id);

            return clientResult;
        }

		/// <summary>
        /// ویرایش آیتم برنامه هفتگی
        /// </summary>
        /// <param name="programItemViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(ProgramItemUpdateViewModel programItemViewModel)
        {
            var programItem = Mapper.Map<ProgramItem>(programItemViewModel);
            _uow.MarkAsChanged(programItem);
			
			 var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(programItem.Id);

            return clientResult;
        }

		/// <summary>
        /// حذف آیتم برنامه هفتگی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  programItemViewModel = GetById(id);
            if (programItemViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var programItem = Mapper.Map<ProgramItem>(programItemViewModel);
            _uow.MarkAsDeleted(programItem);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }
	}
}
