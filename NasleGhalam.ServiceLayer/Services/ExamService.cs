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
                .AsNoTracking()
                .AsEnumerable()
                .Select(current => new ExamViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    PDate = current.Date.ToPersianDate(),
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
            //var a = _exams.Select(current => new
            //{
            //    current.Id,
            //    current.Name,
            //    current.Date,
            //    current.EducationGroupId,
            //    EducationGroupName = current.EducationGroup.Name,
            //    current.EducationYearId,
            //    EducationYearName = current.EducationYear.Name
            //}).AsEnumerable();
            //a.First();
            //return _exams.Select(current => new
            //{
            //    current.Id,
            //    current.Name,
            //    current.Date,
            //    current.EducationGroupId,
            //    EducationGroupName = current.EducationGroup.Name,
            //    current.EducationYearId,
            //    EducationYearName = current.EducationYear.Name
            //}).AsEnumerable()
            //.Select(current => new ExamViewModel()
            //{
            //    Id = current.Id,
            //    Name = current.Name,
            //    PDate = current.Date.ToPersianDate(),
            //    EducationGroupId = current.EducationGroupId,
            //    EducationGroupName = current.EducationGroupName,
            //    EducationYearId = current.EducationYearId,
            //    EducationYearName = current.EducationYearName
            //})
            //.ToList();
        }


        /// <summary>
        /// ثبت امتحان
        /// </summary>
        /// <param name="examViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(ExamViewModel examViewModel)
        {
            var exam = Mapper.Map<Exam>(examViewModel);
            _exams.Add(exam);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = exam.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش امتحان
        /// </summary>
        /// <param name="examViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(ExamViewModel examViewModel)
        {
            var exam = Mapper.Map<Exam>(examViewModel);
            _uow.MarkAsChanged(exam);


            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف امتحان
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
          public MessageResultClient Delete(int id)
        {
            var examViewModel = GetById(id);
            if (examViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var exam = Mapper.Map<Exam>(examViewModel);
            _uow.MarkAsDeleted(exam);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
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
