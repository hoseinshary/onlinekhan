using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Student;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class StudentController : ApiController
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet, CheckUserAccess(ActionBits.StudentReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_studentService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.StudentReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.StudentCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(StudentViewModel studentViewModel)
        {
            var msgRes = _studentService.Create(studentViewModel);
            return Ok(new MessageResultClient
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.StudentUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(StudentViewModel studentViewModel)
        {
            var msgRes = _studentService.Update(studentViewModel);
            return Ok(new MessageResultClient
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.StudentDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _studentService.Delete(id);
            return Ok(new MessageResultClient
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
