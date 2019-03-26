using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Publisher;

namespace NasleGhalam.ServiceLayer.Services
{
    public class PublisherService
    {
        private const string Title = "انتشارات";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Publisher> _publishers;

        public PublisherService(IUnitOfWork uow)
        {
            _uow = uow;
            _publishers = uow.Set<Publisher>();
        }


        /// <summary>
        /// گرفتن  انتشارات با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PublisherViewModel GetById(int id)
        {
            return _publishers
                .Where(current => current.Id == id)
                .Select(current => new PublisherViewModel
                {
                    Id = current.Id,
                    Name = current.Name
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه انتشارات ها
        /// </summary>
        /// <returns></returns>
        public IList<PublisherViewModel> GetAll()
        {
            return _publishers.Select(current => new PublisherViewModel()
            {
                Id = current.Id,
                Name = current.Name
            }).ToList();
        }


        /// <summary>
        /// ثبت انتشارات
        /// </summary>
        /// <param name="publisherViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(PublisherViewModel publisherViewModel)
        {
            var publisher = Mapper.Map<Publisher>(publisherViewModel);
            _publishers.Add(publisher);

            ServerMessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = publisher.Id;
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// ویرایش انتشارات
        /// </summary>
        /// <param name="publisherViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(PublisherViewModel publisherViewModel)
        {
            var publisher = Mapper.Map<Publisher>(publisherViewModel);
            _uow.MarkAsChanged(publisher);


            ServerMessageResult msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// حذف انتشارات
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var publisherViewModel = GetById(id);
            if (publisherViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var publisher = Mapper.Map<Publisher>(publisherViewModel);
            _uow.MarkAsDeleted(publisher);

            ServerMessageResult msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// گرفتن همه انتشارات ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _publishers.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
