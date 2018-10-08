using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Grade;

namespace NasleGhalam.ServiceLayer.Services
{
    public class GradeService
    {
        private const string Title = "مقطع";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Grade> _grades;

        public GradeService(IUnitOfWork uow)
        {
            
            _uow = uow;
            _grades = uow.Set<Grade>();
        }


        /// <summary>
        /// گرفتن  مقطع با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GradeViewModel GetById(int id)
        {
            return _grades
                .Where(current => current.Id == id)
                .Select(current => new GradeViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Priority = current.Priority,
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه مقطع ها
        /// </summary>
        /// <returns></returns>
        public IList<GradeViewModel> GetAll()
        {
            return _grades.Select(current => new GradeViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                Priority = current.Priority
            }).OrderBy(current => current.Priority ).ToList();
        }


        /// <summary>
        /// ثبت مقطع
        /// </summary>
        /// <param name="gradeViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Create(GradeViewModel gradeViewModel)
        {
            var grade = Mapper.Map<Grade>(gradeViewModel);
            _grades.Add(grade);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = grade.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش مقطع
        /// </summary>
        /// <param name="gradeViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Update(GradeViewModel gradeViewModel)
        {
            var grade = Mapper.Map<Grade>(gradeViewModel);
            _uow.MarkAsChanged(grade);
            return _uow.CommitChanges(CrudType.Update, Title);
            
        }


        /// <summary>
        /// حذف مقطع
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultServer Delete(int id)
        {
            var gradeViewModel = GetById(id);
            if (gradeViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var grade = Mapper.Map<Grade>(gradeViewModel);
            _uow.MarkAsDeleted(grade);
            return _uow.CommitChanges(CrudType.Delete, Title);
            
        }


        /// <summary>
        /// گرفتن همه مقطع ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _grades.Where(x => x.GradeLevels.Any()).Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
