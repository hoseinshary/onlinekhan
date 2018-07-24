using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.03.16
    /// </author>
    public class LessonController : ApiController
    {
        private readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }


        [HttpGet, CheckUserAccess(ActionBits.LessonReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_lessonService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.LessonReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var lesson = _lessonService.GetById(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.LessonCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(LessonViewModel lessonViewModel)
        {
            var msgRes = _lessonService.Create(lessonViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.LessonUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(LessonViewModel lessonViewModel)
        {
            var msgRes = _lessonService.Update(lessonViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.LessonDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _lessonService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
