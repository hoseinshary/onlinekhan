using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;

        private readonly Lazy<EducationTreeService> _educationTreeService;

        public LessonService(IUnitOfWork uow, Lazy<EducationTreeService> educationTreeService)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();

            _educationTreeService = educationTreeService;
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
                .Include(current => current.Ratios.Select(ratio => ratio.EducationSubGroup))
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
        public ClientMessageResult Create(LessonCreateViewModel lessonViewModel)
        {
            var educationTrees = _educationTreeService.Value.GetAll()
                .Where(x => x.LookupId_EducationTreeState == 1034)
                .Select(x => x.Id);

            if (lessonViewModel.EducationTreeIds.Any(educationTreeId => !educationTrees.Contains(educationTreeId)))
            {
                return new ClientMessageResult
                {
                    MessageType = MessageType.Error,
                    Message = "تنها مجاز به انتخاب پایه هستید!"
                };
            }
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _lessons.Add(lesson);

            foreach (var treeId in lessonViewModel.EducationTreeIds)
            {
                var tree = new EducationTree() { Id = treeId };
                _uow.MarkAsUnChanged(tree);
                lesson.EducationTrees.Add(tree);
            }

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(lesson.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش درس
        /// </summary>
        /// <param name="lessonUpdateViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(LessonUpdateViewModel lessonUpdateViewModel)
        {
            var lesson = _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios)
                .First(current => current.Id == lessonUpdateViewModel.Id);

            lesson.IsMain = lessonUpdateViewModel.IsMain;
            lesson.Name = lessonUpdateViewModel.Name;
            lesson.LookupId_Nezam = lessonUpdateViewModel.LookupId_Nezam;
            lesson.NumberOfJudges = lessonUpdateViewModel.NumberOfJudges;

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

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(lesson.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var lesson = _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios)
                .First(current => current.Id == id);

            if (lesson == null)
            {
                return ClientMessageResult.NotFound();
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
            return Mapper.Map<ClientMessageResult>(msgRes);
        }
    }
}
