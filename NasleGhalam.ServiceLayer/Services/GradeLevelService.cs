using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.GradeLevel;

namespace NasleGhalam.ServiceLayer.Services
{
    public class GradeLevelService
    {
        private const string Title = "پایه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<GradeLevel> _gradeLevels;

        public GradeLevelService(IUnitOfWork uow)
        {
            _uow = uow;
            _gradeLevels = uow.Set<GradeLevel>();
        }


        /// <summary>
        /// گرفتن  پایه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GradeLevelViewModel GetById(int id)
        {
            return _gradeLevels
                .Where(current => current.Id == id)
                .Select(current => new GradeLevelViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه پایه ها
        /// </summary>
        /// <returns></returns>
        public IList<GradeLevelViewModel> GetAll()
        {
            return _gradeLevels.Select(current => new GradeLevelViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


        /// <summary>
        /// ثبت پایه
        /// </summary>
        /// <param name="gradeLevelViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(GradeLevelViewModel gradeLevelViewModel)
        {
            var gradeLevel = Mapper.Map<GradeLevel>(gradeLevelViewModel);
            _gradeLevels.Add(gradeLevel);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = gradeLevel.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش پایه
        /// </summary>
        /// <param name="gradeLevelViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(GradeLevelViewModel gradeLevelViewModel)
        {
            var gradeLevel = Mapper.Map<GradeLevel>(gradeLevelViewModel);
            _uow.MarkAsChanged(gradeLevel);
            return _uow.CommitChanges(CrudType.Update, Title);
        }


        /// <summary>
        /// حذف پایه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var gradeLevelViewModel = GetById(id);
            if (gradeLevelViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var gradeLevel = Mapper.Map<GradeLevel>(gradeLevelViewModel);
            _uow.MarkAsDeleted(gradeLevel);
            return _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه پایه ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _gradeLevels.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
