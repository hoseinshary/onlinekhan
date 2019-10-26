using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionAnswerJudge;
using NasleGhalam.WebApi.Extensions;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 1398-08-03
	/// </author>
	public class QuestionAnswerJudgeController : ApiController
    {
        private readonly QuestionAnswerJudgeService _questionAnswerJudgeService;
        public QuestionAnswerJudgeController(QuestionAnswerJudgeService questionAnswerJudgeService)
        {
            _questionAnswerJudgeService = questionAnswerJudgeService;
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerJudgeReadAccess)]
        public IHttpActionResult GetAllByQuestionAnswerId(int id)
        {
            return Ok(_questionAnswerJudgeService.GetAllByQuestionAnswerId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerJudgeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionAnswerJudge = _questionAnswerJudgeService.GetById(id);
            if (questionAnswerJudge == null)
            {
                return NotFound();
            }
            return Ok(questionAnswerJudge);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerJudgeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionAnswerJudgeCreateViewModel questionAnswerJudgeViewModel)
        {
            questionAnswerJudgeViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerJudgeService.Create(questionAnswerJudgeViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerJudgeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionAnswerJudgeUpdateViewModel questionAnswerJudgeViewModel)
        {
            questionAnswerJudgeViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerJudgeService.Update(questionAnswerJudgeViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.QuestionAnswerJudgeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionAnswerJudgeService.Delete(id));
        }
    }
}
