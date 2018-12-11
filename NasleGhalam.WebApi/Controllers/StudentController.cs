using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Student;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: علیرضا اعتمادی
	///     date: 1397.07.20
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
        public IHttpActionResult Create(StudentCreateViewModel studentViewModel)
        {
            return Ok(_studentService.Create(studentViewModel, Request.GetRoleLevel()));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.StudentUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(StudentUpdateViewModel studentViewModel)
        {
            return Ok(_studentService.Update(studentViewModel, Request.GetRoleLevel()));
        }


        [HttpPost, CheckUserAccess(ActionBits.StudentDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_studentService.Delete(id));
        }
    }
}
