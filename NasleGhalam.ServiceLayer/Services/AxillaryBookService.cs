using System;
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
        /// <returns></returns>
        public AxillaryBookViewModel GetById(int id)
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
                    BookTypeName = current.Lookup_BookType.Name,
                    LookupId_PaperType = current.LookupId_PaperType,
                    PaperTypeName = current.Lookup_PaperType.Name,
                    LookupId_PrintType = current.LookupId_PrintType,
                    PrintTypeName = current.Lookup_PrintType.Name,
                    PublisherId = current.PublisherId,
                    PublisherName = current.Publisher.Name,
                    HasImage = current.HasImage,
                    ImgPath = current.ImgPath
                    
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه کتاب کمک درسی ها
        /// </summary>
        /// <returns></returns>
        public IList<AxillaryBookViewModel> GetAll()
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
                LookupId_BookType = current.LookupId_BookType,
                LookupId_PaperType = current.LookupId_PaperType,
                LookupId_PrintType = current.LookupId_PrintType,
                PublisherId = current.PublisherId,
                HasImage = current.HasImage,
                ImgPath = current.ImgPath
            }).ToList();
        }


        /// <summary>
        /// ثبت کتاب کمک درسی
        /// </summary>
        /// <param name="axillaryBookViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            _axillaryBooks.Add(axillaryBook);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = axillaryBook.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش کتاب کمک درسی
        /// </summary>
        /// <param name="axillaryBookViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            if (!axillaryBook.HasImage)
            {
                _uow.ExcludeFieldsFromUpdate(axillaryBook, x => x.ImgPath, x => x.HasImage);
            }
            else
            {
                _uow.MarkAsChanged(axillaryBook);
            }
            
            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف کتاب کمک درسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var axillaryBookViewModel = GetById(id);
            if (axillaryBookViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            _uow.MarkAsDeleted(axillaryBook);

            return _uow.CommitChanges(CrudType.Delete, Title);

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
