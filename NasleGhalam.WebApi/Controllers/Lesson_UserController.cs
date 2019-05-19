using System.Collections.Generic;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Lesson;

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
        public IHttpActionResult GetAllByLessonIds (IEnumerable<int> ids)
        {
            return Ok(_lesson_UserService.GetAllByLessonIds(ids));
        }

        [HttpGet, CheckUserAccess(ActionBits.Lesson_UserReadAccess)]
        public IHttpActionResult GetAllByUserIds(IEnumerable<int> ids)
        {
            return Ok(_lesson_UserService.GetAllByUserIds(ids));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.Lesson_UserCreateDeleteAccess)]
        [CheckModelValidation]
        public IHttpActionResult SubmitChanges(Lesson_UserViewModel lesson_UserViewModel)
        {
            return Ok(_lesson_UserService.SubmitChanges(lesson_UserViewModel));
        }     
    }
}
