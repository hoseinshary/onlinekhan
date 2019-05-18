using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Lesson_User;

namespace NasleGhalam.ServiceLayer.Services
{
	public class Lesson_UserService
	{
		private const string Title = "اختصاص کاربر به درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson_User> _lesson_Users;
       
	    public Lesson_UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _lesson_Users = uow.Set<Lesson_User>();
        }


		/// <summary>
        /// گرفتن  اختصاص کاربر به درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Lesson_UserViewModel GetById(int id)
        {
            return _lesson_Users
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<Lesson_UserViewModel>)
                .FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها
        /// </summary>
        /// <returns></returns>
        public IList<Lesson_UserViewModel> GetAll()
        {
            return _lesson_Users
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<Lesson_UserViewModel>)
                .ToList();
        }


		/// <summary>
        /// ثبت اختصاص کاربر به درس
        /// </summary>
        /// <param name="lesson_UserViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(Lesson_UserCreateViewModel lesson_UserViewModel)
        {
            var lesson_User = Mapper.Map<Lesson_User>(lesson_UserViewModel);
            _lesson_Users.Add(lesson_User);

			var msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = lesson_User.Id;
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


		/// <summary>
        /// ویرایش اختصاص کاربر به درس
        /// </summary>
        /// <param name="lesson_UserViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(Lesson_UserUpdateViewModel lesson_UserViewModel)
        {
            var lesson_User = Mapper.Map<Lesson_User>(lesson_UserViewModel);
            _uow.MarkAsChanged(lesson_User);
			
			var msgRes = _uow.CommitChanges(CrudType.Update, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }


		/// <summary>
        /// حذف اختصاص کاربر به درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  lesson_UserViewModel = GetById(id);
            if (lesson_UserViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var lesson_User = Mapper.Map<Lesson_User>(lesson_UserViewModel);
            _uow.MarkAsDeleted(lesson_User);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _lesson_Users.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
