using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.WebApi.Extensions;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شری
	///     date: 06/02/98
	/// </author>
	public class QuestionAnswerController : ApiController
    {
        private readonly QuestionAnswerService _questionAnswerService;
        public QuestionAnswerController(QuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public IHttpActionResult GetAllByQuestionId(int id)
        {
            return Ok(_questionAnswerService.GetAllByQuestionId(id));
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
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult Create([FromUri]QuestionAnswerCreateViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionAnswerViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerService.Create(questionAnswerViewModel,wordFile));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult CreateMulti([FromUri]QuestionAnswerCreateMultiViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionAnswerViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerService.CreateMulti(questionAnswerViewModel,wordFile));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult PreCreateMulti([FromUri]QuestionAnswerCreateMultiViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            return Ok(_questionAnswerService.PreCreateMulti(questionAnswerViewModel,wordFile));
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
