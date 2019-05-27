using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.LessonDepartment;

namespace NasleGhalam.ServiceLayer.Services
{
	public class LessonDepartmentService
	{
		private const string Title = "بخش";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<LessonDepartment> _lessonDepartments;
       
	    public LessonDepartmentService(IUnitOfWork uow)
        {
            _uow = uow;
            _lessonDepartments = uow.Set<LessonDepartment>();
        }

		/// <summary>
        /// گرفتن  بخش با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonDepartmentViewModel GetById(int id)
        {
            return _lessonDepartments
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonDepartmentViewModel>)
                .FirstOrDefault();
        }

		/// <summary>
        /// گرفتن همه بخش ها
        /// </summary>
        /// <returns></returns>
        public IList<LessonDepartmentViewModel> GetAll()
        {
            return _lessonDepartments
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonDepartmentViewModel>)
                .ToList();
        }

		/// <summary>
        /// ثبت بخش
        /// </summary>
        /// <param name="lessonDepartmentViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(LessonDepartmentCreateViewModel lessonDepartmentViewModel)
        {
            var lessonDepartment = Mapper.Map<LessonDepartment>(lessonDepartmentViewModel);
            _lessonDepartments.Add(lessonDepartment);

			var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(lessonDepartment.Id);

            return clientResult;
        }

		/// <summary>
        /// ویرایش بخش
        /// </summary>
        /// <param name="lessonDepartmentViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(LessonDepartmentUpdateViewModel lessonDepartmentViewModel)
        {
            var lessonDepartment = Mapper.Map<LessonDepartment>(lessonDepartmentViewModel);
            _uow.MarkAsChanged(lessonDepartment);
			
			 var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(lessonDepartment.Id);

            return clientResult;
        }

		/// <summary>
        /// حذف بخش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  lessonDepartmentViewModel = GetById(id);
            if (lessonDepartmentViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var lessonDepartment = Mapper.Map<LessonDepartment>(lessonDepartmentViewModel);
            _uow.MarkAsDeleted(lessonDepartment);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }
	}
}
