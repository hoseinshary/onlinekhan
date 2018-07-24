using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationSubGroupService
    {
        private const string Title = "زیر گروه آموزشی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationSubGroup> _educationSubGroups;

        public EducationSubGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationSubGroups = uow.Set<EducationSubGroup>();
        }


        /// <summary>
        /// گرفتن  زیر گروه آموزشی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationSubGroupViewModel GetById(int id)
        {
            return _educationSubGroups
                .Where(current => current.Id == id)
                .Select(current => new EducationSubGroupViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    EducationGroupId = current.EducationGroupId
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه زیر گروه آموزشی ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationSubGroupViewModel> GetAll()
        {
            return _educationSubGroups.Select(current => new EducationSubGroupViewModel()
            {
                
                Id = current.Id,
                Name = current.Name,
                EducationGroupId = current.EducationGroupId,
                EducationGroupName = current.EducationGroup.Name
            }).ToList();
        }


        /// <summary>
        /// ثبت زیر گروه آموزشی
        /// </summary>
        /// <param name="educationSubGroupViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _educationSubGroups.Add(educationSubGroup);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationSubGroup.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش زیر گروه آموزشی
        /// </summary>
        /// <param name="educationSubGroupViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _uow.MarkAsChanged(educationSubGroup);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف زیر گروه آموزشی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var educationSubGroupViewModel = GetById(id);
            if (educationSubGroupViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _uow.MarkAsDeleted(educationSubGroup);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه زیر گروه آموزشی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationSubGroups.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
