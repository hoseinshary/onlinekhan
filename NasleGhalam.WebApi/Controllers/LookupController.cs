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
        public IHttpActionResult GetAllAnswerTypeDdl() 
        {
            return Ok(_lookupService.GetAllDdlByName("AnswerType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookCreateAccess , ActionBits.AxillaryBookReadAccess , ActionBits.AxillaryBookUpdateAccess)]
        public IHttpActionResult GetAllPaperTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("PaperType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookCreateAccess, ActionBits.AxillaryBookReadAccess, ActionBits.AxillaryBookUpdateAccess)]
        public IHttpActionResult GetAllPrintTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("PrintType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonCreateAccess, ActionBits.LessonUpdateAccess, ActionBits.LessonReadAccess)]
        public IHttpActionResult GetAllNezamDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("Nezam"));
        }


        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookCreateAccess, ActionBits.AxillaryBookReadAccess, ActionBits.AxillaryBookUpdateAccess)]
        public IHttpActionResult GetAllBookTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("BookType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess,ActionBits.QuestionReadAccess , ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllQuestionTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("QuestionType"));
        }


        [HttpGet, CheckUserAccess(ActionBits.TopicCreateAccess,ActionBits.TopicReadAccess,ActionBits.TopicUpdateAccess)]
        public IHttpActionResult GetAllTopicHardnessTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("TopicHardnessType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllQuestionHardnessTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("QuestionHardnessType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess, ActionBits.TopicCreateAccess, ActionBits.TopicReadAccess, ActionBits.TopicUpdateAccess)]
        public IHttpActionResult GetAllAreaTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("AreaType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllRepeatnessTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("RepeatnessType"));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionCreateAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionUpdateAccess)]
        public IHttpActionResult GetAllAuthorTypeDdl()
        {
            return Ok(_lookupService.GetAllDdlByName("AuthorType"));
        }


        //[HttpGet, CheckUserAccess(ActionBits.LookupReadAccess)]
        //public IHttpActionResult GetAll()
        //{
        //    return Ok(_lookupService.GetAll());
        //}


        //[HttpGet, CheckUserAccess(ActionBits.LookupReadAccess)]
        //public IHttpActionResult GetById(int id)
        //{
        //    var lookup = _lookupService.GetById(id);
        //    if (lookup == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(lookup);
        //}


        //[HttpPost]
        //[CheckUserAccess(ActionBits.LookupCreateAccess)]
        //[CheckModelValidation]
        //public IHttpActionResult Create(LookupViewModel lookupViewModel)
        //{
        //    var msgRes = _lookupService.Create(lookupViewModel);
        //    return Ok(new MessageResultApi
        //    {
        //        Message = msgRes.FaMessage,
        //        MessageType = msgRes.MessageType,
        //        Id = msgRes.Id
        //    });
        //}


        //[HttpPost]
        //[CheckUserAccess(ActionBits.LookupUpdateAccess)]
        //[CheckModelValidation]
        //public IHttpActionResult Update(LookupViewModel lookupViewModel)
        //{
        //    var msgRes = _lookupService.Update(lookupViewModel);
        //    return Ok(new MessageResultApi
        //    {
        //        Message = msgRes.FaMessage,
        //        MessageType = msgRes.MessageType
        //    });
        //}


        //[HttpPost, CheckUserAccess(ActionBits.LookupDeleteAccess)]
        //public IHttpActionResult Delete(int id)
        //{
        //    var msgRes = _lookupService.Delete(id);
        //    return Ok(new MessageResultApi
        //    {
        //        Message = msgRes.FaMessage,
        //        MessageType = msgRes.MessageType
        //    });
        //}
    }
}
