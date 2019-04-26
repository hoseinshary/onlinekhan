using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class QuestionAnswerController : ApiController
	{
        private readonly QuestionAnswerService _questionAnswerService;
		public QuestionAnswerController(QuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }


		[HttpGet, CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_questionAnswerService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionAnswer = _questionAnswerService.GetById(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }
            return Ok(questionAnswer);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionAnswerCreateViewModel questionAnswerViewModel)
        {
            return Ok(_questionAnswerService.Create(questionAnswerViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionAnswerUpdateViewModel questionAnswerViewModel)
        {
            return Ok(_questionAnswerService.Update(questionAnswerViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionAnswerDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionAnswerService.Delete(id));
        }
	}
}
