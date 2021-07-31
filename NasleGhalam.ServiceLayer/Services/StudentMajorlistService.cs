using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.StudentMajorlist;

namespace NasleGhalam.ServiceLayer.Services
{
    public class StudentMajorlistService
    {
        private const string Title = "انتخاب رشته";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<StudentMajorlist> _stduentMajorlists;
        private readonly IDbSet<Student> _students;
        private readonly IDbSet<Majors> _majors;


        public StudentMajorlistService(IUnitOfWork uow)
        {
            _uow = uow;
            _stduentMajorlists = uow.Set<StudentMajorlist>();
            _students = uow.Set<Student>();
            _majors = uow.Set<Majors>();
        }

        /// <summary>
        /// گرفتن  انتخاب رشته با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentMajorlistGetViewModel GetById(int id)
        {
            return _stduentMajorlists
                .Where(current => current.Id == id)
                .Include(current => current.Majors)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentMajorlistGetViewModel>)
                .FirstOrDefault();
        }
        public IList<StudentMajorlistGetStudentViewModel> GetStudentById(int id, byte roles)
        {
            if (roles < 3)
            {
                return _stduentMajorlists
                .Include(current=> current.Student)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentMajorlistGetStudentViewModel>)
                .ToList();
            }
            else
            {
                return _stduentMajorlists
                    .Where(current => current.StudentId == id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<StudentMajorlistGetStudentViewModel>)
                    .ToList();
            }
        }
        public IList<MajorViewModel> GetStudentMajorsById(int id)
        {
            return Mapper.Map<IList<MajorViewModel>>(_stduentMajorlists
                .Where(current => current.StudentId == id)
                .Include(current => current.Majors)
                .AsNoTracking()
                .AsEnumerable()
                .Select(x=>x.Majors).FirstOrDefault());
        }

        public IList<MajorViewModel> GetAllMajors()
        {
            return _majors
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MajorViewModel>)
                .ToList();
        }
        public IList<MajorViewModel> GetMajorById(int id)
        {
            return _majors
                .Where(x => x.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MajorViewModel>)
                .ToList();
        }
        public IList<MajorViewModel> GetMajorsBySearch(string text)
        {
            var majo = _majors.ToList();
            return _majors
                .Where(x=> x.MajorTitle.Contains(text) || x.University.Contains(text))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MajorViewModel>)
                .ToList();
        }
        /// <summary>
        /// گرفتن همه انتخاب رشته ها
        /// </summary>
        /// <returns></returns>
        public IList<StudentMajorlistViewModel> GetAll()
        {
            return _stduentMajorlists
                .Include(current => current.Majors)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentMajorlistViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت انتخاب رشته
        /// </summary>
        /// <param name="stduentMajorlistViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(StudentMajorlistViewModel stduentMajorlistViewModel)
        {
            StudentMajorlist studentMajorlist = new StudentMajorlist();
            studentMajorlist.Title = stduentMajorlistViewModel.Title;
            studentMajorlist.StudentId = stduentMajorlistViewModel.StudentId;
            studentMajorlist.CreationDate = DateTime.Now;
            foreach (var item in stduentMajorlistViewModel.MajorsId)
            {
                var Major = new Majors() { Id = item };
                _uow.MarkAsUnChanged(Major);
                studentMajorlist.Majors.Add(Major);
            }
            
            _stduentMajorlists.Add(studentMajorlist);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(studentMajorlist.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش انتخاب رشته
        /// </summary>
        /// <param name="stduentMajorlistViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(StudentMajorlistUpdateViewModel stduentMajorlistViewModel)
        {
            var studentmajorlist = _stduentMajorlists
               .Include(current => current.Majors)
               .First(current => current.Id == stduentMajorlistViewModel.Id);
            studentmajorlist.Title = stduentMajorlistViewModel.Title;
            var deletemajorList = studentmajorlist.Majors
                 .Where(oldMaj => stduentMajorlistViewModel.MajorsId.All(newMajId => newMajId != oldMaj.Id))
                 .ToList();
            foreach (var item in deletemajorList)
            {
                studentmajorlist.Majors.Remove(item);
            }
            var addmajorList = stduentMajorlistViewModel.MajorsId
               .Where(oldMajId => studentmajorlist.Majors.All(newMaj => newMaj.Id != oldMajId))
               .ToList();
            foreach (var item in addmajorList)
            {
                var Major = new Majors() { Id = item };
                _uow.MarkAsUnChanged(Major);
                studentmajorlist.Majors.Add(Major);
            }
            _uow.MarkAsChanged(studentmajorlist);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(studentmajorlist.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف انتخاب رشته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var studentmajorlist = _stduentMajorlists
               .Include(current => current.Majors)
               .First(current => current.Id == id);
            if (studentmajorlist == null)
            {
                return ClientMessageResult.NotFound();
            }
            var stduentMajorlist = Mapper.Map<StudentMajorlist>(studentmajorlist);
            var majors = stduentMajorlist.Majors.ToList();
            foreach (var item in majors)
            {
                stduentMajorlist.Majors.Remove(item);

            }
            _uow.MarkAsDeleted(stduentMajorlist);
            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }
    }
}
