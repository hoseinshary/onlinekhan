using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Writer;

namespace NasleGhalam.ServiceLayer.Services
{
	public class WriterService
	{
		private const string Title = "نویسنده";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Writer> _writers;
       
	    public WriterService(IUnitOfWork uow)
        {
            _uow = uow;
            _writers = uow.Set<Writer>();
        }


		/// <summary>
        /// گرفتن  نویسنده با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WriterViewModel GetById(int id)
        {
            return _writers
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<WriterViewModel>)
                .FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه نویسنده ها
        /// </summary>
        /// <returns></returns>
        public IList<WriterViewModel> GetAll()
        {
            return _writers
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<WriterViewModel>)
                .ToList();
        }


		/// <summary>
        /// ثبت نویسنده
        /// </summary>
        /// <param name="writerViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(WriterCreateViewModel writerViewModel)
        {
            var writer = Mapper.Map<Writer>(writerViewModel);
            _writers.Add(writer);

			var msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = writer.Id;
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


		/// <summary>
        /// ویرایش نویسنده
        /// </summary>
        /// <param name="writerViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(WriterUpdateViewModel writerViewModel)
        {
            var writer = Mapper.Map<Writer>(writerViewModel);
            _uow.MarkAsChanged(writer);
			
			var msgRes = _uow.CommitChanges(CrudType.Update, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }


		/// <summary>
        /// حذف نویسنده
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  writerViewModel = GetById(id);
            if (writerViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var writer = Mapper.Map<Writer>(writerViewModel);
            _uow.MarkAsDeleted(writer);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// گرفتن همه نویسنده ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _writers.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
