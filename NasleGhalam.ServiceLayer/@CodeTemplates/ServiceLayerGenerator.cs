using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Media;

namespace NasleGhalam.ServiceLayer.Services
{
	public class MediaService
	{
		private const string Title = "رسانه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Media> _medias;
       
	    public MediaService(IUnitOfWork uow)
        {
            _uow = uow;
            _medias = uow.Set<Media>();
        }

		/// <summary>
        /// گرفتن  رسانه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MediaViewModel GetById(int id)
        {
            return _medias
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MediaViewModel>)
                .FirstOrDefault();
        }

		/// <summary>
        /// گرفتن همه رسانه ها
        /// </summary>
        /// <returns></returns>
        public IList<MediaViewModel> GetAll()
        {
            return _medias
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MediaViewModel>)
                .ToList();
        }

		/// <summary>
        /// ثبت رسانه
        /// </summary>
        /// <param name="mediaViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(MediaCreateViewModel mediaViewModel)
        {
            var media = Mapper.Map<Media>(mediaViewModel);
            _medias.Add(media);

			var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(media.Id);

            return clientResult;
        }

		/// <summary>
        /// ویرایش رسانه
        /// </summary>
        /// <param name="mediaViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(MediaUpdateViewModel mediaViewModel)
        {
            var media = Mapper.Map<Media>(mediaViewModel);
            _uow.MarkAsChanged(media);
			
			 var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(media.Id);

            return clientResult;
        }

		/// <summary>
        /// حذف رسانه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  mediaViewModel = GetById(id);
            if (mediaViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var media = Mapper.Map<Media>(mediaViewModel);
            _uow.MarkAsDeleted(media);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }
	}
}
