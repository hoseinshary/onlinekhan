using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationGroup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationGroupService
    {
        private const string Title = "گروه آموزشی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationGroup> _educationGroups;

        public EducationGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationGroups = uow.Set<EducationGroup>();
        }


        /// <summary>
        /// گرفتن  گروه آموزشی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationGroupViewModel GetById(int id)
        {
            return _educationGroups
                .Where(current => current.Id == id)
                .Select(current => new EducationGroupViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه گروه آموزشی ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationGroupViewModel> GetAll()
        {
            return _educationGroups.Select(current => new EducationGroupViewModel()
            {
                Id = current.Id,
                Name = current.Name               
            }).ToList();
        }


        /// <summary>
        /// ثبت گروه آموزشی
        /// </summary>
        /// <param name="educationGroupViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(EducationGroupViewModel educationGroupViewModel)
        {
            var educationGroup = Mapper.Map<EducationGroup>(educationGroupViewModel);
            _educationGroups.Add(educationGroup);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationGroup.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش گروه آموزشی
        /// </summary>
        /// <param name="educationGroupViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(EducationGroupViewModel educationGroupViewModel)
        {
            var educationGroup = Mapper.Map<EducationGroup>(educationGroupViewModel);
            _uow.MarkAsChanged(educationGroup);
            return _uow.CommitChanges(CrudType.Update, Title);
            
        }


        /// <summary>
        /// حذف گروه آموزشی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var educationGroupViewModel = GetById(id);
            if (educationGroupViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var educationGroup = Mapper.Map<EducationGroup>(educationGroupViewModel);
            _uow.MarkAsDeleted(educationGroup);
            return _uow.CommitChanges(CrudType.Delete, Title);
            
        }


        /// <summary>
        /// گرفتن همه گروه آموزشی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationGroups.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
