using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Province;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ProvinceService
    {
        private const string Title = "استان";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Province> _provinces;
       

        public ProvinceService(IUnitOfWork uow )
        {
            _uow = uow;
            _provinces = uow.Set<Province>();

        }


        /// <summary>
        /// گرفتن  استان با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProvinceViewModel GetById(int id)
        {
            return _provinces
                .Where(current => current.Id == id)
                .Select(current => new ProvinceViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Code = current.Code,
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه استان ها
        /// </summary>
        /// <returns></returns>
        public IList<ProvinceViewModel> GetAll()
        {
            return _provinces.Select(current => new ProvinceViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                Code = current.Code

            }).ToList();
        }


        /// <summary>
        /// ثبت استان
        /// </summary>
        /// <param name="provinceViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(ProvinceViewModel provinceViewModel)
        {
            var province = Mapper.Map<Province>(provinceViewModel);
            _provinces.Add(province);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = province.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش استان
        /// </summary>
        /// <param name="provinceViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(ProvinceViewModel provinceViewModel)
        {
            var province = Mapper.Map<Province>(provinceViewModel);
            _uow.MarkAsChanged(province);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف استان
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var provinceViewModel = GetById(id);
            if (provinceViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var province = Mapper.Map<Province>(provinceViewModel);
            _uow.MarkAsDeleted(province);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه استان ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _provinces.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
