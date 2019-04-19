using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری
	///     date: 29/5/1397
	/// </author>
	public class LookupController : ApiController
    {
        private readonly LookupService _lookupService;
        public LookupController(LookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet, CheckUserAccess(ActionBits.AnswerCreateAccess ,ActionBits.AnswerReadAccess , ActionBits.AnswerUpdateAccess)] 
        public IHttpActionResult GetAllAnswerType() 
        {
            return Ok(_lookupService.GetAllByName("AnswerType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationTreeCreateAccess, ActionBits.EducationTreeReadAccess, ActionBits.EducationTreeUpdateAccess)]
        public IHttpActionResult GetAllEducationTreeState()
        {
            return Ok(_lookupService.GetAllByName("EducationTreeState"));
        }

        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookCreateAccess , ActionBits.AxillaryBookReadAccess , ActionBits.AxillaryBookUpdateAccess)]
        public IHttpActionResult GetAllPaperType()
        {
            return Ok(_lookupService.GetAllByName("PaperType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookCreateAccess, ActionBits.AxillaryBookReadAccess, ActionBits.AxillaryBookUpdateAccess)]
        public IHttpActionResult GetAllPrintType()
        {
            return Ok(_lookupService.GetAllByName("PrintType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonCreateAccess, ActionBits.LessonUpdateAccess, ActionBits.LessonReadAccess)]
        public IHttpActionResult GetAllNezam()
        {
            return Ok(_lookupService.GetAllByName("Nezam"));
        }

        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookCreateAccess, ActionBits.AxillaryBookReadAccess, ActionBits.AxillaryBookUpdateAccess)]
        public IHttpActionResult GetAllBookType()
        {
            return Ok(_lookupService.GetAllByName("BookType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess,ActionBits.QuestionReadAccess , ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllQuestionType()
        {
            return Ok(_lookupService.GetAllByName("QuestionType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicCreateAccess,ActionBits.TopicReadAccess,ActionBits.TopicUpdateAccess)]
        public IHttpActionResult GetAllTopicHardnessType()
        {
            return Ok(_lookupService.GetAllByName("TopicHardnessType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllQuestionHardnessType()
        {
            return Ok(_lookupService.GetAllByName("QuestionHardnessType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess, ActionBits.TopicCreateAccess, ActionBits.TopicReadAccess, ActionBits.TopicUpdateAccess)]
        public IHttpActionResult GetAllAreaType()
        {
            return Ok(_lookupService.GetAllByName("AreaType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllRepeatnessType()
        {
            return Ok(_lookupService.GetAllByName("RepeatnessType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllAuthorType()
        {
            return Ok(_lookupService.GetAllByName("AuthorType"));
        }
    }
}
