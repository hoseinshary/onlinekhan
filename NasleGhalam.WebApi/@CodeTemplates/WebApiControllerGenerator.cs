using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.LessonDepartment;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class LessonDepartmentController : ApiController
	{
        private readonly LessonDepartmentService _lessonDepartmentService;
		public LessonDepartmentController(LessonDepartmentService lessonDepartmentService)
        {
            _lessonDepartmentService = lessonDepartmentService;
        }

		[HttpGet, CheckUserAccess(ActionBits.LessonDepartmentReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_lessonDepartmentService.GetAll());
        }

		[HttpGet, CheckUserAccess(ActionBits.LessonDepartmentReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var lessonDepartment = _lessonDepartmentService.GetById(id);
            if (lessonDepartment == null)
            {
                return NotFound();
            }
            return Ok(lessonDepartment);
        }

		[HttpPost]
        [CheckUserAccess(ActionBits.LessonDepartmentCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(LessonDepartmentCreateViewModel lessonDepartmentViewModel)
        {
            return Ok(_lessonDepartmentService.Create(lessonDepartmentViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonDepartmentUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(LessonDepartmentUpdateViewModel lessonDepartmentViewModel)
        {
            return Ok(_lessonDepartmentService.Update(lessonDepartmentViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.LessonDepartmentDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_lessonDepartmentService.Delete(id));
        }
	}
}
