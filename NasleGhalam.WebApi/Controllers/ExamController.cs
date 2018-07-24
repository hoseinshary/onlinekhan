using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Exam;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class ExamController : ApiController
    {
        private readonly ExamService _ExamService;
        public ExamController(ExamService ExamService)
        {
            _ExamService = ExamService;
        }


        [HttpGet, CheckUserAccess(ActionBits.ExamReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_ExamService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.ExamReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var Exam = _ExamService.GetById(id);
            if (Exam == null)
            {
                return NotFound();
            }
            return Ok(Exam);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.ExamCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ExamViewModel ExamViewModel)
        {
            var msgRes = _ExamService.Create(ExamViewModel);
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
        public IHttpActionResult Update(ExamViewModel ExamViewModel)
        {
            var msgRes = _ExamService.Update(ExamViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.ExamDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _ExamService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
