using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationYear;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationYearService
    {
        private const string Title = "سال تحصیلی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationYear> _educationYears;

        public EducationYearService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationYears = uow.Set<EducationYear>();
        }


        /// <summary>
        /// گرفتن  سال تحصیلی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationYearViewModel GetById(int id)
        {
            return _educationYears
                .Where(current => current.Id == id)
                .Select(current => new EducationYearViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    IsActiveYear = current.IsActiveYear
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه سال تحصیلی ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationYearViewModel> GetAll()
        {
            return _educationYears.Select(current => new EducationYearViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                IsActiveYear = current.IsActiveYear
            }).ToList();
        }


        /// <summary>
        /// ثبت سال تحصیلی
        /// </summary>
        /// <param name="educationYearViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationYearViewModel educationYearViewModel)
        {
            var transacion = _uow.BeginTransaction();
            if (educationYearViewModel.IsActiveYear)
            {
                _uow.ExecuteSqlCommand("update EducationYears set IsActiveYear = 0");
                //_educationYears.ToList().ForEach(current => current.IsActiveYear = false);
            }
            var educationYear = Mapper.Map<EducationYear>(educationYearViewModel);
            _educationYears.Add(educationYear);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationYear.Id;
            if (msgRes.MessageType == MessageType.Success)
                transacion.Commit();
            else
                transacion.Rollback();

            return msgRes;

        }

        /// <summary>
        /// ویرایش سال تحصیلی
        /// </summary>
        /// <param name="educationYearViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationYearViewModel educationYearViewModel)
        {
            var transacion = _uow.BeginTransaction();
            if(educationYearViewModel.IsActiveYear)
            {
                _uow.ExecuteSqlCommand("update EducationYears set IsActiveYear = 0");
            }
            var educationYear = Mapper.Map<EducationYear>(educationYearViewModel);
            _uow.MarkAsChanged(educationYear);
            var result = _uow.CommitChanges(CrudType.Update, Title);

            if (result.MessageType == MessageType.Success)
                transacion.Commit();
            else
                transacion.Rollback();

            return result;
        }


        /// <summary>
        /// حذف سال تحصیلی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var educationYearViewModel = GetById(id);
            if (educationYearViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var educationYear = Mapper.Map<EducationYear>(educationYearViewModel);
            _uow.MarkAsDeleted(educationYear);

            return _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه سال تحصیلی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationYears.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
