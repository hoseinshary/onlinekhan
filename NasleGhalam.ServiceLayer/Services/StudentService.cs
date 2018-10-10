using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                .Include(current => current.User)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .ProjectTo<StudentViewModel>()
               .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه دانش آموز ها
        /// </summary>
        /// <returns></returns>
        public IList<StudentViewModel> GetAll()
        {
            var a = _students
                .Include(current => current.User.City)
                .Include(current => current.User.Role)
                .AsNoTracking()
                .ToList();

            var a1 = Mapper.Map<IList<StudentViewModel>>(a);

            var b = _students
                .AsNoTracking()
                .ToList();

            // lazy loading
            //var b1 = b.First().User.Role.Name;

            return _students
                //.Include(current => current.User)
                .AsNoTracking()
                .ProjectTo<StudentViewModel>()
                .ToList();
        }


        /// <summary>
        /// ثبت دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Create(StudentViewModel studentViewModel)
        {
            var student = Mapper.Map<Student>(studentViewModel);
            _students.Add(student);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = student.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <returns></returns>
        public MessageResultServer Update(StudentViewModel studentViewModel)
        {
            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsChanged(student);

            return _uow.CommitChanges(CrudType.Update, Title);
        }


        /// <summary>
        /// حذف دانش آموز
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultServer Delete(int id)
        {
            var studentViewModel = GetById(id);
            if (studentViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsDeleted(student);

            return _uow.CommitChanges(CrudType.Delete, Title);
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
