using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.UniversityBranch;

namespace NasleGhalam.ServiceLayer.Services
{
    public class UniversityBranchService
    {
        private const string Title = "رشته دانشگاهی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<UniversityBranch> _universityBranchs;

        public UniversityBranchService(IUnitOfWork uow)
        {
            _uow = uow;
            _universityBranchs = uow.Set<UniversityBranch>();
        }

        /// <summary>
        /// گرفتن همه رشته دانشگاهی ها با ای دی گروه آموزشی
        /// </summary>
        /// <returns></returns>
        public IList<UniversityBranchViewModel> GetAllByEducationGroupId(int id )
        {
            return _universityBranchs.Where(current => current.EducationSubGroup.EducationGroupId == id ).Select(current => new UniversityBranchViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                SiteAverage = current.SiteAverage,
                Balance1Low = current.Balance1Low,
                Balance1High = current.Balance1High,
                Balance2Low = current.Balance2Low,
                Balance2High = current.Balance2High,
                EducationSubGroupId = current.EducationSubGroupId,
                EducationSubGroupName = current.EducationSubGroup.Name
            }).ToList();
        }


        /// <summary>
        /// گرفتن  رشته دانشگاهی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UniversityBranchViewModel GetById(int id)
        {
            return _universityBranchs
                .Where(current => current.Id == id)
                .Select(current => new UniversityBranchViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    SiteAverage = current.SiteAverage,
                    Balance1Low = current.Balance1Low,
                    Balance1High = current.Balance1High,
                    Balance2Low = current.Balance2Low,
                    Balance2High = current.Balance2High,
                    EducationSubGroupId = current.EducationSubGroupId,
                    EducationSubGroupName = current.EducationSubGroup.Name
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه رشته دانشگاهی ها
        /// </summary>
        /// <returns></returns>
        public IList<UniversityBranchViewModel> GetAll()
        {
            return _universityBranchs.Select(current => new UniversityBranchViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                SiteAverage = current.SiteAverage,
                Balance1Low = current.Balance1Low,
                Balance1High = current.Balance1High,
                Balance2Low = current.Balance2Low,
                Balance2High = current.Balance2High,
                EducationSubGroupId = current.EducationSubGroupId
            }).ToList();
        }


        /// <summary>
        /// ثبت رشته دانشگاهی
        /// </summary>
        /// <param name="universityBranchViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Create(UniversityBranchViewModel universityBranchViewModel)
        {
            var universityBranch = Mapper.Map<UniversityBranch>(universityBranchViewModel);
            _universityBranchs.Add(universityBranch);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = universityBranch.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش رشته دانشگاهی
        /// </summary>
        /// <param name="universityBranchViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Update(UniversityBranchViewModel universityBranchViewModel)
        {
            var universityBranch = Mapper.Map<UniversityBranch>(universityBranchViewModel);
            _uow.MarkAsChanged(universityBranch);

            return _uow.CommitChanges(CrudType.Update, Title);
        }


        /// <summary>
        /// حذف رشته دانشگاهی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultServer Delete(int id)
        {
            var universityBranchViewModel = GetById(id);
            if (universityBranchViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var universityBranch = Mapper.Map<UniversityBranch>(universityBranchViewModel);
            _uow.MarkAsDeleted(universityBranch);

            return _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه رشته دانشگاهی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _universityBranchs.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
