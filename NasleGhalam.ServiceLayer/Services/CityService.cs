using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.City;

namespace NasleGhalam.ServiceLayer.Services
{
    public class CityService
    {
        private const string Title = "شهر";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<City> _Citys;

        public CityService(IUnitOfWork uow)
        {
            _uow = uow;
            _Citys = uow.Set<City>();
        }


        /// <summary>
        /// گرفتن  شهر با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CityViewModel GetById(int id)
        {
            return _Citys
                .Where(current => current.Id == id)
                .Select(current => new CityViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    ProvinceId = current.ProvinceId,
                    ProvinceName = current.Province.Name
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه شهر ها
        /// </summary>
        /// <returns></returns>
        public IList<CityViewModel> GetAll()
        {
            return _Citys.Select(current => new CityViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                ProvinceId = current.ProvinceId,
                ProvinceName = current.Province.Name
            }).ToList();
        }


        /// <summary>
        /// ثبت شهر
        /// </summary>
        /// <param name="CityViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(CityViewModel CityViewModel)
        {
            var City = Mapper.Map<City>(CityViewModel);
            _Citys.Add(City);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = City.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش شهر
        /// </summary>
        /// <param name="CityViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(CityViewModel CityViewModel)
        {
            var City = Mapper.Map<City>(CityViewModel);
            _uow.MarkAsChanged(City);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف شهر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var CityViewModel = GetById(id);
            if (CityViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var City = Mapper.Map<City>(CityViewModel);
            _uow.MarkAsDeleted(City);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه شهر ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _Citys.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
