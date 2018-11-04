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
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;

        public LessonService(IUnitOfWork uow)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
        }


        /// <summary>
        /// گرفتن  درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonViewModel GetById(int id)
        {
            return _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios.Select(ratio => ratio.EducationSubGroup))
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه درس ها با آی دی درخت آموزشی
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAllByEducationTreeIds(IEnumerable<int> ids)
        {
            return _lessons
                .Where(current => current.EducationTrees.Any(x => ids.Contains(x.Id)))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(LessonCreateViewModel lessonViewModel)
        {
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _lessons.Add(lesson);

            //var educationTrees = _uow.Set<EducationTree>();
            foreach (var TreeId in lessonViewModel.EducationTreeIds)
            {
                var tree = new EducationTree() { Id = TreeId };
                //educationTrees.Attach(tree);
                _uow.MarkAsUnChanged(tree);
                lesson.EducationTrees.Add(tree);
            }




            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = lesson.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(LessonUpdateViewModel lessonUpdateViewModel)
        {




            var exLesson = GetById(lessonUpdateViewModel.Id);

            var lesson = Mapper.Map<Lesson>(lessonUpdateViewModel);
            _uow.MarkAsChanged(lesson);

            //delete all education tree
            var list = exLesson.EducationTrees.ToList();
            foreach (var item in list)
            {
                var educationTree = Mapper.Map<EducationTree>(item);
                _uow.MarkAsDeleted(educationTree);
            }



            //insert education tree
            foreach (var educationTree in lessonUpdateViewModel.EducationTreeIds)
            {
                var tree = new EducationTree() { Id = educationTree };
                //_uow.MarkAsUnChanged(tree);
                lesson.EducationTrees.Add(tree);
            }


            //delete ratio

            var deleteRatio = exLesson.Ratios
                .Where(x => !lessonUpdateViewModel.Ratios.Any(y => y.Id == x.Id))
                .ToList();

            foreach (var exRatio in deleteRatio)
            {

                _uow.MarkAsDeleted(exRatio);

            }

            //add ratio
            var addRatio = lessonUpdateViewModel.Ratios
                .Where(x => exLesson.Ratios.Any(y => y.Id != x.Id)).ToList();


            foreach (var newRatio in addRatio)
            {
                var ratio = Mapper.Map<Ratio>(newRatio);
                lesson.Ratios.Add(ratio);

            }

            //update ratio
            var updateRatio = exLesson.Ratios
                .Where(x => lessonUpdateViewModel.Ratios.Any(y => y.Id == x.Id))
                .ToList();
            foreach (var ratio in updateRatio)
            {
                _uow.MarkAsChanged(ratio);

            }




            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var lessonViewModel = GetById(id);
            if (lessonViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _uow.MarkAsDeleted(lesson);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// گرفتن همه درس ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _lessons.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
