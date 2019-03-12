using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionJudge;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionJudgeService
    {
        private const string Title = "کارشناسی سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionJudge> _questionJudges;
        private readonly IDbSet<Question> _questions;
        private readonly IDbSet<Lookup> _lookups;

        private const int NumberOfJudges = 3;

        public QuestionJudgeService(IUnitOfWork uow)
        {
            _uow = uow;
            _questionJudges = uow.Set<QuestionJudge>();
            _questions = uow.Set<Question>();
            _lookups = uow.Set<Lookup>();
        }


        /// <summary>
        /// گرفتن  کارشناسی سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionJudgeViewModel GetById(int id)
        {
            return _questionJudges
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionJudgeViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه کارشناسی سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionJudgeViewModel> GetAllByQuestionId(int questionId)
        {
            return _questionJudges
                .Where(current => current.QuestionId == questionId)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionJudgeViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت کارشناسی سوال
        /// </summary>
        /// <param name="questionJudgeViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(QuestionJudgeCreateViewModel questionJudgeViewModel, int userid)
        {

            var questionJudge = Mapper.Map<QuestionJudge>(questionJudgeViewModel);
            questionJudge.UserId = userid;
            _questionJudges.Add(questionJudge);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);

            if (msgRes.MessageType == MessageType.Success)
            {
                if (_questionJudges.Count(current => current.QuestionId == questionJudgeViewModel.QuestionId) >= NumberOfJudges)
                {
                    var questionJudges = _questionJudges
                        .Where(current => current.QuestionId == questionJudgeViewModel.QuestionId)
                        .Include(current => current.Lookup_QuestionHardnessType)
                        .Include(current => current.Lookup_RepeatnessType)
                        .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

                    double lookup_questionhardness = 0;
                    double lookup_repeatness = 0;
                    int count_isStandard = 0;
                    int count_isDelete = 0;
                    int count_isUpdate = 0;
                    int count_isLearning = 0;
                    int responseTime = 0;

                    foreach (var judge in questionJudges)
                    {
                        if (judge.IsDelete == true)
                            count_isDelete++;
                        if (judge.IsUpdate == true)
                            count_isUpdate++;
                        if (judge.IsStandard == true)
                            count_isStandard++;
                        if (judge.IsLearning == true)
                            count_isLearning++;

                        lookup_questionhardness += judge.Lookup_QuestionHardnessType.State;
                        lookup_repeatness += judge.Lookup_RepeatnessType.State;

                        responseTime += judge.ResponseSecond;
                    }

                    var updateQuestion = _questions.First(x => x.Id == questionJudgeViewModel.QuestionId);
                    updateQuestion.ResponseSecond = Convert.ToInt16( responseTime / NumberOfJudges);
                    if (count_isStandard > NumberOfJudges / 2)
                        updateQuestion.IsStandard = true;
                    else
                        updateQuestion.IsStandard = false;

                    if (count_isLearning > NumberOfJudges / 2)
                        updateQuestion.IsLearning = true;
                    else
                        updateQuestion.IsLearning = false;

                    if (count_isDelete > NumberOfJudges / 2)
                        updateQuestion.IsDelete = true;
                    else
                        updateQuestion.IsDelete = false;

                    if (count_isUpdate > NumberOfJudges / 2)
                        updateQuestion.IsUpdate = true;
                    else
                        updateQuestion.IsUpdate = false;

                    updateQuestion.LookupId_QuestionHardnessType = _lookups
                        .First(x=> x.Name=="QuestionHardnessType" && x.State == (int)Math.Round(lookup_questionhardness/NumberOfJudges))
                        .Id;

                    updateQuestion.LookupId_RepeatnessType= _lookups
                        .First(x => x.Name == "RepeatnessType" && x.State == (int)Math.Round(lookup_repeatness / NumberOfJudges))
                        .Id;

                    _uow.MarkAsChanged(updateQuestion);
                    _uow.ValidateOnSaveEnabled(false);
                    var msgResUpdate = _uow.CommitChanges(CrudType.Update, Title);
                }
            }

            msgRes.Id = questionJudge.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش کارشناسی سوال
        /// </summary>
        /// <param name="questionJudgeViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(QuestionJudgeUpdateViewModel questionJudgeViewModel)
        {
            var questionJudge = Mapper.Map<QuestionJudge>(questionJudgeViewModel);
            _uow.MarkAsChanged(questionJudge);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);

            if (msgRes.MessageType == MessageType.Success)
            {
                if (_questionJudges.Count(current => current.QuestionId == questionJudgeViewModel.QuestionId) >= NumberOfJudges)
                {
                    var questionJudges = _questionJudges
                        .Where(current => current.QuestionId == questionJudgeViewModel.QuestionId)
                        .Include(current => current.Lookup_QuestionHardnessType)
                        .Include(current => current.Lookup_RepeatnessType)
                        .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

                    double lookup_questionhardness = 0;
                    double lookup_repeatness = 0;
                    int count_isStandard = 0;
                    int count_isDelete = 0;
                    int count_isUpdate = 0;
                    int count_isLearning = 0;
                    int responseTime = 0;

                    foreach (var judge in questionJudges)
                    {
                        if (judge.IsDelete == true)
                            count_isDelete++;
                        if (judge.IsUpdate == true)
                            count_isUpdate++;
                        if (judge.IsStandard == true)
                            count_isStandard++;
                        if (judge.IsLearning == true)
                            count_isLearning++;

                        lookup_questionhardness += judge.Lookup_QuestionHardnessType.State;
                        lookup_repeatness += judge.Lookup_RepeatnessType.State;

                        responseTime += judge.ResponseSecond;
                    }

                    var updateQuestion = _questions.First(x => x.Id == questionJudgeViewModel.QuestionId);
                    updateQuestion.ResponseSecond = Convert.ToInt16(responseTime / NumberOfJudges);
                    if (count_isStandard > NumberOfJudges / 2)
                        updateQuestion.IsStandard = true;
                    else
                        updateQuestion.IsStandard = false;

                    if (count_isLearning > NumberOfJudges / 2)
                        updateQuestion.IsLearning = true;
                    else
                        updateQuestion.IsLearning = false;

                    if (count_isDelete > NumberOfJudges / 2)
                        updateQuestion.IsDelete = true;
                    else
                        updateQuestion.IsDelete = false;

                    if (count_isUpdate > NumberOfJudges / 2)
                        updateQuestion.IsUpdate = true;
                    else
                        updateQuestion.IsUpdate = false;

                    updateQuestion.LookupId_QuestionHardnessType = _lookups
                        .First(x => x.Name == "QuestionHardnessType" && x.State == (int)Math.Round(lookup_questionhardness / NumberOfJudges))
                        .Id;

                    updateQuestion.LookupId_RepeatnessType = _lookups
                        .First(x => x.Name == "RepeatnessType" && x.State == (int)Math.Round(lookup_repeatness / NumberOfJudges))
                        .Id;

                    _uow.MarkAsChanged(updateQuestion);
                    _uow.ValidateOnSaveEnabled(false);
                    var msgResUpdate = _uow.CommitChanges(CrudType.Update, Title);
                }
            }

            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف کارشناسی سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var questionJudgeViewModel = GetById(id);
            if (questionJudgeViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var questionJudge = Mapper.Map<QuestionJudge>(questionJudgeViewModel);
            _uow.MarkAsDeleted(questionJudge);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


    
    }
}
