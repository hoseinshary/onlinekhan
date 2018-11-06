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

            foreach (var treeId in lessonViewModel.EducationTreeIds)
            {
                var tree = new EducationTree() { Id = treeId };
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
        /// <param name="lessonUpdateViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(LessonUpdateViewModel lessonUpdateViewModel)
        {
            var lesson = _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios)
                .First(current => current.Id == lessonUpdateViewModel.Id);

            lesson.IsMain = lessonUpdateViewModel.IsMain;
            lesson.Name = lessonUpdateViewModel.Name;
            lesson.LookupId_Nezam = lessonUpdateViewModel.LookupId_Nezam;

            //delete education tree
            var deleteEducationTreeList = lesson.EducationTrees
                .Where(oldEdu => lessonUpdateViewModel.EducationTreeIds.All(newEduId => newEduId != oldEdu.Id))
                .ToList();
            foreach (var educationTree in deleteEducationTreeList)
            {
                lesson.EducationTrees.Remove(educationTree);
            }

            //add education tree
            var addEducationTreeList = lessonUpdateViewModel.EducationTreeIds
                .Where(oldEduId => lesson.EducationTrees.All(newEdu => newEdu.Id != oldEduId))
                .ToList();
            foreach (var educationTreeId in addEducationTreeList)
            {
                var educationTree = new EducationTree { Id = educationTreeId };
                _uow.MarkAsUnChanged(educationTree);
                lesson.EducationTrees.Add(educationTree);
            }

            //delete ratio
            var deleteRatio = lesson.Ratios
                .Where(x => lessonUpdateViewModel.Ratios.All(y => y.Id != x.Id))
                .ToList();
            foreach (var ratio in deleteRatio)
            {
                _uow.MarkAsDeleted(ratio);
            }

            //update ratio
            var updateRatio = lessonUpdateViewModel.Ratios
                .Where(x => lesson.Ratios.Any(y => y.Id == x.Id))
                .ToList();
            foreach (var ratioViewModel in updateRatio)
            {
                var ratio = lesson.Ratios.First(x => x.Id == ratioViewModel.Id);
                ratio.EducationSubGroupId = ratioViewModel.EducationSubGroupId;
                ratio.LessonId = ratioViewModel.LessonId;
                ratio.Rate = ratioViewModel.Rate;
            }

            //add ratio
            var addRatio = lessonUpdateViewModel.Ratios
                .Where(x => lesson.Ratios.All(y => y.Id != x.Id))
                .ToList();
            foreach (var newRatio in addRatio)
            {
                var ratio = Mapper.Map<Ratio>(newRatio);
                lesson.Ratios.Add(ratio);
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
            var lesson = _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios)
                .First(current => current.Id == id);

            if (lesson == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            //remove education tree 
            var educationTrees = lesson.EducationTrees.ToList();
            foreach (var item in educationTrees)
            {
                lesson.EducationTrees.Remove(item);
                
            }

            //remove ratios
            var ratios = lesson.Ratios.ToList();
            foreach (var item in ratios)
            {
                _uow.MarkAsDeleted(item);
            }


            
            

            
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
