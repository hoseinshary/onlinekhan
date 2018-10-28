using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.EducationTree;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationTreeService
    {
        private const string Title = "درخت آموزش";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationTree> _educationTrees;

        public EducationTreeService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationTrees = uow.Set<EducationTree>();
        }


        /// <summary>
        /// گرفتن  درخت آموزش با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationTreeGetByIdViewModel GetById(int id)
        {
            return _educationTrees
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationTreeGetByIdViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه درخت آموزش ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationTreeGetAllViewModel> GetAll()
        {
            return _educationTrees
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationTreeGetAllViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت درخت آموزش
        /// </summary>
        /// <param name="educationTreeViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(EducationTreeCreateViewModel educationTreeViewModel)
        {
            var educationTree = Mapper.Map<EducationTree>(educationTreeViewModel);
            _educationTrees.Add(educationTree);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = educationTree.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش درخت آموزش
        /// </summary>
        /// <param name="educationTreeViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(EducationTreeUpdateViewModel educationTreeViewModel)
        {
            var educationTree = Mapper.Map<EducationTree>(educationTreeViewModel);
            _uow.MarkAsChanged(educationTree);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف درخت آموزش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var educationTreeViewModel = GetById(id);
            if (educationTreeViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var educationTree = Mapper.Map<EducationTree>(educationTreeViewModel);
            _uow.MarkAsDeleted(educationTree);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// گرفتن همه درخت آموزش ها برای لیست کشویی
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
