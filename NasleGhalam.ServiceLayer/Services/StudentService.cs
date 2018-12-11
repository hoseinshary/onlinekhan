using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Student;

namespace NasleGhalam.ServiceLayer.Services
{
    public class StudentService
    {
        private const string Title = "دانش آموز";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Student> _students;
        private readonly Lazy<RoleService> _roleService;

        public StudentService(IUnitOfWork uow,
            Lazy<RoleService> roleService)
        {
            _uow = uow;
            _students = uow.Set<Student>();
            _roleService = roleService;
        }


        /// <summary>
        /// گرفتن  دانش آموز با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentViewModel GetById(int id)
        {
            return _students
                .Include(current => current.User.City)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه دانش آموز ها
        /// </summary>
        /// <returns></returns>
        public IList<StudentViewModel> GetAll()
        {
            return _students
                .Include(current => current.User.City)
                .Include(current => current.User.Role)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResultClient Create(StudentCreateViewModel studentViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(studentViewModel.User.RoleId, userRoleLevel);
            if (role.Level <= userRoleLevel)
            {
                return new MessageResultClient()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }

            var student = Mapper.Map<Student>(studentViewModel);
            student.User.LastLogin = DateTime.Now;
            _students.Add(student);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = student.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResultClient Update(StudentUpdateViewModel studentViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(studentViewModel.User.RoleId, userRoleLevel);
            if (role == null)
            {
                return new MessageResultClient()
                {
                    Message = "نقش یافت نگردید",
                    MessageType = MessageType.Error
                };
            }

            if (role.Level <= userRoleLevel)
            {
                return new MessageResultClient()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }

            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsChanged(student.User);
            _uow.MarkAsChanged(student);

            if (string.IsNullOrEmpty(student.User.Password))
            {
                _uow.ExcludeFieldsFromUpdate(student.User, x => x.Password, x => x.LastLogin);
                _uow.ValidateOnSaveEnabled(false);
            }
            else
            {
                _uow.ExcludeFieldsFromUpdate(student.User, x => x.LastLogin);
            }

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف دانش آموز
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var studentViewModel = GetById(id);
            if (studentViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsDeleted(student);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        ///// <summary>
        ///// گرفتن همه دانش آموز ها برای لیست کشویی
        ///// </summary>
        ///// <returns></returns>
        //public IList<SelectViewModel> GetAllDdl()
        //{
        //    return _students.Select(current => new SelectViewModel
        //    {
        //        value = current.Id,
        //        label = current.Name
        //    }).ToList();
        //}
    }
}
