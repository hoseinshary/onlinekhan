using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class QuestionController : ApiController
	{
        private readonly QuestionService _questionService;
		public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }


		[HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_questionService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var question = _questionService.GetById(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.QuestionCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionCreateViewModel questionViewModel)
        {
            return Ok(_questionService.Create(questionViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionUpdateViewModel questionViewModel)
        {
            return Ok(_questionService.Update(questionViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionService.Delete(id));
        }
	}
}
