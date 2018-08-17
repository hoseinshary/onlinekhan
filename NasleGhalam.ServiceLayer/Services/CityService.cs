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
        private readonly IDbSet<City> _cities;

        public CityService(IUnitOfWork uow)
        {
            _uow = uow;
            _cities = uow.Set<City>();
        }


        /// <summary>
        /// گرفتن  شهر با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CityViewModel GetById(int id)
        {
            return _cities
                .Where(current => current.Id == id)
                .Select(current => new CityViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    ProvinceId = current.ProvinceId,
                    // ProvinceName = current.Province.Name
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه شهر ها
        /// </summary>
        /// <returns></returns>
        public IList<CityViewModel> GetAll()
        {
            return _cities.Select(current => new CityViewModel()
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
        /// <param name="cityViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(CityViewModel cityViewModel)
        {
            var city = Mapper.Map<City>(cityViewModel);
            _cities.Add(city);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = city.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش شهر
        /// </summary>
        /// <param name="cityViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(CityViewModel cityViewModel)
        {
            var city = Mapper.Map<City>(cityViewModel);
            _uow.MarkAsChanged(city);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف شهر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var cityViewModel = GetById(id);
            if (cityViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var city = Mapper.Map<City>(cityViewModel);
            _uow.MarkAsDeleted(city);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه شهر ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllByProvinceIdDdl(int provinceId)
        {
            return _cities
                .Where(current => current.ProvinceId == provinceId)
                .Select(current => new SelectViewModel
                {
                    value = current.Id,
                    label = current.Name
                }).ToList();
        }
    }
}
