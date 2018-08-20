using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Lookup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LookupService
    {
        private const string Title = "لوک آپ";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lookup> _lookups;

        public LookupService(IUnitOfWork uow)
        {
            _uow = uow;
            _lookups = uow.Set<Lookup>();
        }


        /// <summary>
        /// گرفتن  لوک آپ با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LookupViewModel GetById(int id)
        {
            return _lookups
                .Where(current => current.Id == id)
                .Select(current => new LookupViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Value = current.Value,
                    State = current.State
                }).FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه ی لوکاپ ها با نام
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllByName(string name)
        {
            return _lookups.Where(x => x.Name == name)
                .Select(current => new SelectViewModel()
            {
                label = current.Value,
                value = current.Id
            }).ToList();
        }




        /// <summary>
        /// گرفتن همه لوک آپ ها
        /// </summary>
        /// <returns></returns>
        public IList<LookupViewModel> GetAll()
        {
            return _lookups.Select(current => new LookupViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                Value = current.Value,
                State = current.State
            }).ToList();
        }


        /// <summary>
        /// ثبت لوک آپ
        /// </summary>
        /// <param name="lookupViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(LookupViewModel lookupViewModel)
        {
            var lookup = Mapper.Map<Lookup>(lookupViewModel);
            _lookups.Add(lookup);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = lookup.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش لوک آپ
        /// </summary>
        /// <param name="lookupViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(LookupViewModel lookupViewModel)
        {
            var lookup = Mapper.Map<Lookup>(lookupViewModel);
            _uow.MarkAsChanged(lookup);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف لوک آپ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var lookupViewModel = GetById(id);
            if (lookupViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var lookup = Mapper.Map<Lookup>(lookupViewModel);
            _uow.MarkAsDeleted(lookup);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه لوک آپ ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _lookups.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
