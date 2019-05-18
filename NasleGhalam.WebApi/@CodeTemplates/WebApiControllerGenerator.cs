using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Lesson_User;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شری
	///     date: 1398/02/28
	/// </author>
	public class Lesson_UserController : ApiController
	{
        private readonly Lesson_UserService _lesson_UserService;
		public Lesson_UserController(Lesson_UserService lesson_UserService)
        {
            _lesson_UserService = lesson_UserService;
        }


		[HttpGet, CheckUserAccess(ActionBits.Lesson_UserReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_lesson_UserService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.Lesson_UserReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var lesson_User = _lesson_UserService.GetById(id);
            if (lesson_User == null)
            {
                return NotFound();
            }
            return Ok(lesson_User);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.Lesson_UserCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(Lesson_UserCreateViewModel lesson_UserViewModel)
        {
            return Ok(_lesson_UserService.Create(lesson_UserViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.Lesson_UserUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(Lesson_UserUpdateViewModel lesson_UserViewModel)
        {
            return Ok(_lesson_UserService.Update(lesson_UserViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.Lesson_UserDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_lesson_UserService.Delete(id));
        }
	}
}
