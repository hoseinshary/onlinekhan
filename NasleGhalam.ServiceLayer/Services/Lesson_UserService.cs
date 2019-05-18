using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
    public class Lesson_UserService
    {
        private const string Title = "اختصاص کاربر به درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;
        private readonly IDbSet<User> _users;


        public Lesson_UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
            _users = uow.Set<User>();
        }


        


        /// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllByUserIds(IEnumerable<int> ids)
        {
            return _lessons
                .AsNoTracking()
                .AsEnumerable()
                .Where(x=>x.Users.Any(y=>ids.Contains(y.Id)))
                .Select(x=>x.Id)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllByLessonIds(IEnumerable<int> ids)
        {
            return _users
                .AsNoTracking()
                .AsEnumerable()
                .Where(x=>x.Lessons.Any(y=>ids.Contains(y.Id)))
                .Select(x=>x.Id)
                .ToList();
        }

        /// <summary>
        /// ثبت اختصاص کاربر به درس
        /// </summary>
        /// <param name="lesson_UserViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult SubmitChages(Lesson_UserViewModel lesson_UserViewModel)
        {
            var previousLessons = _lessons
                .Where(x => x.Users.Any(y => lesson_UserViewModel.UsersId.Contains(y.Id)));


            var previousUsers = _users
                .Where(x => x.Lessons.Any(y => lesson_UserViewModel.LessonsId.Contains(y.Id)));
                




            foreach (var userId in lesson_UserViewModel.LessonsId)
            {
                var user = _users.First(x => x.Id == userId);
                foreach (var lessonId in lesson_UserViewModel.UsersId)
                {
                    var lesson = new Lesson() {Id = lessonId};
                    _uow.MarkAsUnChanged(lesson);
                    user.Lessons.Add(lesson);
                }
            }
            

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        
        

   
    }
}
