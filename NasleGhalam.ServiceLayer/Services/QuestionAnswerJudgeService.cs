using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionAnswerJudge;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionAnswerJudgeService
    {
        private const string Title = "کارشناسی جواب سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionAnswerJudge> _questionAnswerJudges;
        private readonly IDbSet<QuestionAnswer> _questionAnswer;

        public QuestionAnswerJudgeService(IUnitOfWork uow)
        {
            _uow = uow;
            _questionAnswerJudges = uow.Set<QuestionAnswerJudge>();
            _questionAnswer = uow.Set<QuestionAnswer>();
        }

        /// <summary>
        /// گرفتن  کارشناسی جواب سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionAnswerJudgeViewModel GetById(int id)
        {
            return _questionAnswerJudges
                .Include(current => current.User)
                .Include(current => current.Lookup_ReasonProblem)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerJudgeViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه کارشناسی جواب سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionAnswerJudgeViewModel> GetAllByQuestionAnswerId(int questionAnswerId)
        {
            return _questionAnswerJudges
                .Include(current => current.User)
                .Include(current => current.Lookup_ReasonProblem)
                .Where(current => current.QuestionAnswerId == questionAnswerId)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerJudgeViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت کارشناسی جواب سوال
        /// </summary>
        /// <param name="questionAnswerJudgeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionAnswerJudgeCreateViewModel questionAnswerJudgeViewModel)
        {
            var questionAnswer = _questionAnswer.Where(x => x.Id == questionAnswerJudgeViewModel.QuestionAnswerId)
                .Include(x => x.QuestionAnswerJudges).FirstOrDefault();

            if (questionAnswer.QuestionAnswerJudges.Any(x => x.UserId == questionAnswerJudgeViewModel.UserId))
            {
                return new ClientMessageResult()
                {
                    Message = $"کارشناسان اجازه ثبت بیش از یک کارشناسی ندارند!",
                    MessageType = MessageType.Error
                };
            }
            

            var questionAnswerJudge = Mapper.Map<QuestionAnswerJudge>(questionAnswerJudgeViewModel);
            _questionAnswerJudges.Add(questionAnswerJudge);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionAnswerJudge.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش کارشناسی جواب سوال
        /// </summary>
        /// <param name="questionAnswerJudgeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionAnswerJudgeUpdateViewModel questionAnswerJudgeViewModel)
        {
            var questionAnswerJudge = Mapper.Map<QuestionAnswerJudge>(questionAnswerJudgeViewModel);
            _uow.MarkAsChanged(questionAnswerJudge);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionAnswerJudge.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف کارشناسی جواب سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var questionAnswerJudgeViewModel = GetById(id);
            if (questionAnswerJudgeViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var questionAnswerJudge = Mapper.Map<QuestionAnswerJudge>(questionAnswerJudgeViewModel);
            _uow.MarkAsDeleted(questionAnswerJudge);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }
    }
}
