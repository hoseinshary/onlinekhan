using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: علیرضا اعتمادی
	///     date: 1397.03.16
	/// </author>
	public class LessonController : ApiController
    {
        private const short ReadAccess = 1;
        private const short CreateAccess = 2;
        private const short UpdateAccess = 3;
        private const short DeleteAccess = 4;

        private readonly LessonService _lessonService;
        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }


        [HttpGet, CheckUserAccess(ActionBit = new[] { ReadAccess })]
        public IHttpActionResult GetAll()
        {
            return Ok(_lessonService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBit = new[] { ReadAccess })]
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
        [CheckUserAccess(ActionBit = new[] { CreateAccess })]
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
        [CheckUserAccess(ActionBit = new[] { UpdateAccess })]
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


        [HttpPost, CheckUserAccess(ActionBit = new[] { DeleteAccess })]
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
