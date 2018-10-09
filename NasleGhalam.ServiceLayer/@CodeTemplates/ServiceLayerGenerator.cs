using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Student;

namespace NasleGhalam.ServiceLayer.Services
{
	public class StudentService
	{
		private const string Title = "دانش آموز";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Student> _students;
       
	    public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
            _students = uow.Set<Student>();
        }


		/// <summary>
        /// گرفتن  دانش آموز با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentViewModel GetById(int id)
        {
            return _students
                .Where(current => current.Id == id)
                .Select(current => new StudentViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


		/// <summary>
        /// گرفتن همه دانش آموز ها
        /// </summary>
        /// <returns></returns>
        public IList<StudentViewModel> GetAll()
        {
            return _students.Select(current => new StudentViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


		/// <summary>
        /// ثبت دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(StudentViewModel studentViewModel)
        {
            var student = Mapper.Map<Student>(studentViewModel);
            _students.Add(student);

			MessageResult msgRes =  _uow.CommitChanges(CrudType.Create, Title);
			msgRes.Id = student.Id;
            return msgRes;
        }


		/// <summary>
        /// ویرایش دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(StudentViewModel studentViewModel)
        {
            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsChanged(student);
			
			return  _uow.CommitChanges(CrudType.Update, Title);
        }


		/// <summary>
        /// حذف دانش آموز
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
			var  studentViewModel = GetById(id);
            if (studentViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsDeleted(student);
            
			return  _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه دانش آموز ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _students.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
	}
}
