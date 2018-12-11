using System;
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
        private readonly Lazy<EducationGroup_LessonService> _educationGroupLessonService;

        public LessonController(LessonService lessonService,
            Lazy<EducationGroup_LessonService> educationGroupLessonService)
        {
            _lessonService = lessonService;
            _educationGroupLessonService = educationGroupLessonService;
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
        public IHttpActionResult Create2(LessonViewModel lessonViewModel)
        {
            var msgRes = _lessonService.Create(lessonViewModel);
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(LessonCreateAndUpdateViewModel lessonCreateAndUpdateViewModel)
        {
            var msgRes = _lessonService.CreateLessonWithRatio(lessonCreateAndUpdateViewModel);
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.LessonUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(LessonCreateAndUpdateViewModel lessonViewModel)
        {
            var msgRes = _lessonService.Update(lessonViewModel);
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.LessonDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _lessonService.Delete(id);
            return Ok(msgRes);
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonReadAccess,
             ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllDdl()
        {
            return Ok(_lessonService.GetAllDdl());
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationBookCreateAccess, 
            ActionBits.EducationBookUpdateAccess)]
        public IHttpActionResult GetAllByEducationGroupIdDdl(int id)
        {
            return Ok(_educationGroupLessonService.Value.GetAllByEducationGroupIdDdl(id));
        }

    }
}
