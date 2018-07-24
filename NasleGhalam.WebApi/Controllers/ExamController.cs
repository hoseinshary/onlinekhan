using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Exam;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین     
	///     date: 31/04/1397
	/// </author>
	public class ExamController : ApiController
    {
        private readonly ExamService _examService;
        public ExamController(ExamService examService)
        {
            _examService = examService;
        }


        [HttpGet, CheckUserAccess(ActionBits.ExamReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_examService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.ExamReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var exam = _examService.GetById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.ExamCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ExamViewModel examViewModel)
        {
            var msgRes = _examService.Create(examViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.ExamUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(ExamViewModel examViewModel)
        {
            var msgRes = _examService.Update(examViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.ExamDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _examService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
