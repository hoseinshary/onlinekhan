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
    public class EducationTreeService
    {
        private const string Title = "گروه آموزشی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationTree> _educationTrees;
        private readonly IDbSet<EducationSubGroup> _educationSubGroups;

        public EducationTreeService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationTrees = uow.Set<EducationTree>();
            _educationSubGroups = uow.Set<EducationSubGroup>();
        }


        /// <summary>
        /// گرفتن  گروه آموزشی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationGroupViewModel GetById(int id)
        {
            return _educationTrees
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
            return _educationTrees.Select(current => new EducationGroupViewModel()
            {
                Id = current.Id,
                Name = current.Name
            }).ToList();
        }



        /// <summary>
        /// گرفتن همه گروه های آموزشی همراه با زیر گروه آموزشی 
        /// </summary>
        /// <returns></returns>
        public IList<EducationGroupWithSubGroupsViewModel> GetAllWithSubGroups()
        {
            return _educationTrees
                .Where(current => current.EducationSubGroups.Any())
                .Select(current => new EducationGroupWithSubGroupsViewModel()
                {
                    EducationGroupId = current.Id,
                    Name = current.Name,
                    SubGroups = current.EducationSubGroups.Select(x => new EducationSubGroupLessonViewModel()
                    {
                        EducationSubGroupId = x.Id,
                        Name = x.Name,
                    }).ToList()
            }).ToList();
        }



        //public IList<EducationGroupWithSubGroupsViewModel> GetAllEducationGroupWithsubGroups2(int lessonId)
        //{
        //    List<EducationGroupWithSubGroupsViewModel> returnVal = new List<EducationGroupWithSubGroupsViewModel>();
        //    if (lessonId == 0)
        //    {
        //        var educationGroups = _educationGroups.Select(current => new EducationGroupIsCheckedViewModel()
        //        {
        //            EducationGroupId = current.Id,
        //            Name = current.Name
        //        }).ToList();

        //        foreach (var item in educationGroups)
        //        {
        //            int educationGroupkeySearch = item.EducationGroupId;
        //            var subGroups = _educationSubGroups
        //                .Where(current => current.EducationGroupId == educationGroupkeySearch)
        //                .Select(current => new EducationSubGroupLessonViewModel
        //                {

        //                    EducationSubGroupId = current.Id,
        //                    Name = current.Name,


        //                }).ToList();
        //            EducationGroupWithSubGroupsViewModel model = new EducationGroupWithSubGroupsViewModel()
        //            {
        //                EducationGroup = item,
        //                SubGroups = subGroups
        //            };
        //            returnVal.Add(model);
        //        }

        //    }
        //    else
        //    {
        //        EducationGroup_LessonService obj = new EducationGroup_LessonService(_uow);
        //        var educationGroups = obj.GetAllEducationGroupByLessonId(lessonId);
        //        foreach (var item in educationGroups)
        //        {
        //            int educationGroupkeySearch = item.EducationGroupId;
        //            var eduIsChecked = new EducationGroupIsCheckedViewModel()
        //            {
        //                EducationGroupId = item.EducationGroupId,
        //                Name = item.EducatioGroupName,
        //                IsChecked = item.IsChecked
        //            };
        //            var subGroups = _educationSubGroups
        //                .Where(current => current.EducationGroupId == educationGroupkeySearch)
        //                .Select(current => new EducationSubGroupLessonViewModel
        //                {
        //                    EducationSubGroupId = current.Id,
        //                    Name = current.Name


        //                }).ToList();
        //            var model = new EducationGroupWithSubGroupsViewModel()
        //            {
        //                EducationGroup = eduIsChecked,
        //                SubGroups = subGroups
        //            };
        //            returnVal.Add(model);
        //        }
        //    }
        //    return returnVal;
        //}


        /// <summary>
        /// ثبت گروه آموزشی
        /// </summary>
        /// <param name="educationGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(EducationGroupViewModel educationGroupViewModel)
        {
            var educationGroup = Mapper.Map<EducationGroup>(educationGroupViewModel);
            _educationTrees.Add(educationGroup);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationGroup.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش گروه آموزشی
        /// </summary>
        /// <param name="educationGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(EducationGroupViewModel educationGroupViewModel)
        {
            var educationGroup = Mapper.Map<EducationGroup>(educationGroupViewModel);
            _uow.MarkAsChanged(educationGroup);
            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف گروه آموزشی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var educationGroupViewModel = GetById(id);
            if (educationGroupViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var educationGroup = Mapper.Map<EducationGroup>(educationGroupViewModel);
            _uow.MarkAsDeleted(educationGroup);
            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// گرفتن همه گروه آموزشی ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _educationTrees.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }



    }
}
