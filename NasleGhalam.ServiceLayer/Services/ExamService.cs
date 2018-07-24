using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Exam;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ExamService
    {
        private const string Title = "امتحان";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Exam> _exams;

        public ExamService(IUnitOfWork uow)
        {
            _uow = uow;
            _exams = uow.Set<Exam>();
        }


        /// <summary>
        /// گرفتن  امتحان با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamViewModel GetById(int id)
        {
            return _exams
                .Where(current => current.Id == id)
                .Select(current => new ExamViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Date = current.Date,
                    EducationGroupId = current.EducationGroupId,
                   // EducationGroupName = current.EducationGroup.Name,
                    EducationYearId = current.EducationYearId,
                   // EducationYearName = current.EducationYear.Name
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه امتحان ها
        /// </summary>
        /// <returns></returns>
        public IList<ExamViewModel> GetAll()
        {
            return _exams.Select(current => new ExamViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                Date = current.Date,
                EducationGroupId = current.EducationGroupId,
                EducationGroupName = current.EducationGroup.Name,
                EducationYearId = current.EducationYearId,
                EducationYearName = current.EducationYear.Name
            }).ToList();
        }


        /// <summary>
        /// ثبت امتحان
        /// </summary>
        /// <param name="examViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(ExamViewModel examViewModel)
        {
            var exam = Mapper.Map<Exam>(examViewModel);
            _exams.Add(exam);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = exam.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش امتحان
        /// </summary>
        /// <param name="examViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(ExamViewModel examViewModel)
        {
            var exam = Mapper.Map<Exam>(examViewModel);
            _uow.MarkAsChanged(exam);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف امتحان
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var examViewModel = GetById(id);
            if (examViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var exam = Mapper.Map<Exam>(examViewModel);
            _uow.MarkAsDeleted(exam);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه امتحان ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _exams.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
