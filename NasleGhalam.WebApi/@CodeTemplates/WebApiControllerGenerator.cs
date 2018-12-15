using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class QuestionGroupController : ApiController
	{
        private readonly QuestionGroupService _questionGroupService;
		public QuestionGroupController(QuestionGroupService questionGroupService)
        {
            _questionGroupService = questionGroupService;
        }


		[HttpGet, CheckUserAccess(ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_questionGroupService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionGroup = _questionGroupService.GetById(id);
            if (questionGroup == null)
            {
                return NotFound();
            }
            return Ok(questionGroup);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionGroupCreateViewModel questionGroupViewModel)
        {
            return Ok(_questionGroupService.Create(questionGroupViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            return Ok(_questionGroupService.Update(questionGroupViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionGroupService.Delete(id));
        }
	}
}
