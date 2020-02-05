using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Teacher;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 2020.02.2
	/// </author>
	public class TeacherController : ApiController
    {
        private readonly TeacherService _teacherService;
        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet, CheckUserAccess(ActionBits.TeacherReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_teacherService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.TeacherReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var teacher = _teacherService.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TeacherCreateViewModel teacherViewModel)
        {
            return Ok(_teacherService.Create(teacherViewModel, Request.GetRoleLevel()));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TeacherUpdateViewModel teacherViewModel)
        {
            return Ok(_teacherService.Update(teacherViewModel, Request.GetRoleLevel()));
        }

        [HttpPost, CheckUserAccess(ActionBits.TeacherDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_teacherService.Delete(id));
        }
    }
}
