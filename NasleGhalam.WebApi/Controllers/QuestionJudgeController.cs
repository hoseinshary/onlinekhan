using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionJudge;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary  
	///     date: 1397/12/10
	/// </author>
	public class QuestionJudgeController : ApiController
    {
        private readonly QuestionJudgeService _questionJudgeService;
        public QuestionJudgeController(QuestionJudgeService questionJudgeService)
        {
            _questionJudgeService = questionJudgeService;
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionJudgeReadAccess)]
        public IHttpActionResult GetAllByQuestionId(int id)
        {
            return Ok(_questionJudgeService.GetAllByQuestionId(id));
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionJudgeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionJudge = _questionJudgeService.GetById(id);
            if (questionJudge == null)
            {
                return NotFound();
            }
            return Ok(questionJudge);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionJudgeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionJudgeCreateViewModel questionJudgeViewModel)
        {
            return Ok(_questionJudgeService.Create(questionJudgeViewModel));

        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionJudgeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionJudgeUpdateViewModel questionJudgeViewModel)
        {
            return Ok(_questionJudgeService.Update(questionJudgeViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionJudgeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionJudgeService.Delete(id));
        }
    }
}
