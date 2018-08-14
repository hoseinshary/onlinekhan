using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationGroup;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationGroupService
    {
        private const string Title = "گروه آموزشی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationGroup> _educationGroups;
        private readonly IDbSet<EducationSubGroup> _educationSubGroups;

        public EducationGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationGroups = uow.Set<EducationGroup>();
            _educationSubGroups = uow.Set<EducationSubGroup>();
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

        public IList<EducationGroupWithSubGroupsViewModel> GetAllEducationGroupWithsubGroups(int lessonId)
        {
            List<EducationGroupWithSubGroupsViewModel> returnVal = new List<EducationGroupWithSubGroupsViewModel>();
            if (lessonId == 0)
            {
                var educationGroups = _educationGroups.Select(current => new EducationGroupIsCheckedViewModel()
                {
                    Id = current.Id,
                    Name = current.Name
                }).ToList();

                foreach (var item in educationGroups)
                {
                    int educationGroupkeySearch = item.Id;
                    var subGroups = _educationSubGroups
                        .Where(current => current.EducationGroupId == educationGroupkeySearch)
                        .Select(current => new EducationSubGroupViewModel
                        {
                            Id = current.Id,
                            Name = current.Name,
                            EducationGroupId = current.EducationGroupId,

                        }).ToList();
                    EducationGroupWithSubGroupsViewModel model = new EducationGroupWithSubGroupsViewModel()
                    {
                        EducationGroups = item,
                        SubGroups = subGroups
                    };
                    returnVal.Add(model);
                }
            
            }
            else
            {
                EducationGroup_LessonService obj = new EducationGroup_LessonService(_uow);
                var educationGroups = obj.GetAllEducationGroupByLessonId(lessonId);
                foreach (var item in educationGroups)
                {
                    int educationGroupkeySearch = item.EducationGroupId;
                    var eduIsChecked = new EducationGroupIsCheckedViewModel()
                    {
                        Id = item.EducationGroupId,
                        Name = item.EducatioGroupName,
                        IsChecked = item.IsChecked
                    };
                    var subGroups = _educationSubGroups
                        .Where(current => current.EducationGroupId == educationGroupkeySearch)
                        .Select(current => new EducationSubGroupViewModel
                        {
                            Id = current.Id,
                            Name = current.Name,
                            EducationGroupId = current.EducationGroupId,

                        }).ToList();
                    var model = new EducationGroupWithSubGroupsViewModel()
                    {
                        EducationGroups = eduIsChecked,
                        SubGroups = subGroups
                    };
                    returnVal.Add(model);
                }
            }
            return returnVal;
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
