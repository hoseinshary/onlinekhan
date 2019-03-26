using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.AxillaryBook;

namespace NasleGhalam.ServiceLayer.Services
{
    public class AxillaryBookService
    {
        private const string Title = "کتاب کمک درسی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AxillaryBook> _axillaryBooks;

        public AxillaryBookService(IUnitOfWork uow)
        {
            _uow = uow;
            _axillaryBooks = uow.Set<AxillaryBook>();
        }


        /// <summary>
        /// گرفتن  کتاب کمک درسی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imgUrlPath"></param>
        /// <returns></returns>
        public AxillaryBookViewModel GetById(int id, string imgUrlPath = "")
        {
            return _axillaryBooks
                .Where(current => current.Id == id)
                .Select(current => new AxillaryBookViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Author = current.Author,
                    PublishYear = current.PublishYear,
                    Description = current.Description,
                    Font = current.Font,
                    Isbn = current.Isbn,
                    Price = current.Price,
                    OriginalPrice = current.OriginalPrice,
                    LookupId_BookType = current.LookupId_BookType,
                    LookupId_PaperType = current.LookupId_PaperType,
                    LookupId_PrintType = current.LookupId_PrintType,
                    PublisherId = current.PublisherId,
                    ImgName = current.ImgName,
                    ImgPath = string.IsNullOrEmpty(current.ImgName) ? "" : imgUrlPath + current.ImgName
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه کتاب کمک درسی ها
        /// </summary>
        /// <returns></returns>
        public IList<AxillaryBookViewModel> GetAll(string imgUrlPath)
        {
            return _axillaryBooks.Select(current => new AxillaryBookViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                Author = current.Author,
                PublishYear = current.PublishYear,
                Description = current.Description,
                Font = current.Font,
                Isbn = current.Isbn,
                Price = current.Price,
                OriginalPrice = current.OriginalPrice,
                BookTypeName = current.Lookup_BookType.Value,
                PaperTypeName = current.Lookup_PaperType.Value,
                PrintTypeName = current.Lookup_PrintType.Value,
                PublisherName = current.Publisher.Name,
                ImgPath = string.IsNullOrEmpty(current.ImgName) ? "" : imgUrlPath + current.ImgName
            }).ToList();
        }


        /// <summary>
        /// ثبت کتاب کمک درسی
        /// </summary>
        /// <param name="axillaryBookViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            _axillaryBooks.Add(axillaryBook);

            ServerMessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = axillaryBook.Id;

            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// ویرایش کتاب کمک درسی
        /// </summary>
        /// <param name="axillaryBookViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            if (string.IsNullOrEmpty(axillaryBook.ImgName))
            {
                _uow.ExcludeFieldsFromUpdate(axillaryBook, x => x.ImgName);
            }
            else
            {
                _uow.MarkAsChanged(axillaryBook);
            }

            ServerMessageResult msgRes = _uow.CommitChanges(CrudType.Update, Title);

            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// حذف کتاب کمک درسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var axillaryBookViewModel = GetById(id);
            if (axillaryBookViewModel == null)
            {
                 
                return ClientMessageResult.NotFound();
            }

            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            _uow.MarkAsDeleted(axillaryBook);
            ServerMessageResult msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// گرفتن همه کتاب کمک درسی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _axillaryBooks.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
