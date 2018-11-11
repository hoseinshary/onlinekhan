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
        private const string Title = "زیر گروه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationSubGroup> _educationSubGroups;

        public EducationSubGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationSubGroups = uow.Set<EducationSubGroup>();
        }


        /// <summary>
        /// گرفتن  زیر گروه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationSubGroupViewModel GetById(int id)
        {
            return _educationSubGroups
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationSubGroupViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه زیر گروه ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationSubGroupViewModel> GetAll()
        {
            return _educationSubGroups
                .Include(current => current.EducationTree)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationSubGroupViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت زیر گروه
        /// </summary>
        /// <param name="educationSubGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _educationSubGroups.Add(educationSubGroup);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationSubGroup.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش زیر گروه
        /// </summary>
        /// <param name="educationSubGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _uow.MarkAsChanged(educationSubGroup);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف زیر گروه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var educationSubGroupViewModel = GetById(id);
            if (educationSubGroupViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _uow.MarkAsDeleted(educationSubGroup);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// گرفتن همه زیر گروه های یک گروه آموزشی برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllByEducationTreeIdDdl(int educationTreeId)
        {
            return _educationSubGroups
                .Where(current => current.EducationTreeId == educationTreeId)
                .Select(current => new SelectViewModel
                {
                    value = current.Id,
                    label = current.Name
                }).ToList();
        }
    }
}
