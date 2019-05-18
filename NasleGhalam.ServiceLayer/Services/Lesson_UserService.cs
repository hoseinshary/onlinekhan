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
        public ClientMessageResult SubmitChanges(Lesson_UserViewModel lesson_UserViewModel)
        {
            var previousLessons = _lessons
                .AsNoTracking()
                .Where(x => x.Users.Any(y => lesson_UserViewModel.UsersId.Contains(y.Id)));


            var previousUsers = _users
                .AsNoTracking()
                .Where(x => x.Lessons.Any(y => lesson_UserViewModel.LessonsId.Contains(y.Id)));

            //حذف
            foreach (var user in previousUsers)
            {
                foreach (var lesson in previousLessons)
                {
                    if (lesson_UserViewModel.LessonsId.All(x => x != lesson.Id))
                        user.Lessons.Remove(lesson);
                }
            }

            foreach (var lesson in previousLessons)
            {
                foreach (var user in previousUsers)
                {
                    if (lesson_UserViewModel.UsersId.All(x => x != user.Id))
                        lesson.Users.Remove(user);
                }
            }

            //add
            foreach (var  userId in lesson_UserViewModel.UsersId)
            {
                var user = _users.AsNoTracking().First(x => x.Id == userId);
                foreach (var lessonId in lesson_UserViewModel.LessonsId)
                {
                    if (previousLessons.All(x => x.Id != lessonId))
                    {
                        Lesson lesson = new Lesson() {Id = lessonId};
                        _uow.MarkAsUnChanged(lesson);
                        user.Lessons.Add(lesson);
                    }
                }
            }


            foreach (var lessonId in lesson_UserViewModel.LessonsId)
            {
                var lesson = _lessons.AsNoTracking().First(x => x.Id == lessonId);
                foreach (var userId in lesson_UserViewModel.UsersId)
                {
                    if (previousUsers.All(x => x.Id != userId))
                    {
                        User user = new User() {Id = userId};
                        _uow.MarkAsUnChanged(user);
                        lesson.Users.Add(user);
                    }
                }
            }


            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        
        

   
    }
}
