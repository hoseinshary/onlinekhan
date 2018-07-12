using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationGroup_Lesson;
using NasleGhalam.WebApi.Extentions;
using System.Collections.Generic;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری  
	///     date: 10/4/1397
	/// </author>
	public class EducationGroup_LessonController : ApiController
    {
        private readonly EducationGroup_LessonService _educationGroup_LessonService;
        public EducationGroup_LessonController(EducationGroup_LessonService educationGroup_LessonService)
        {
            _educationGroup_LessonService = educationGroup_LessonService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationGroup_LessonReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationGroup_LessonService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationGroup_LessonReadAccess)]
        public IHttpActionResult GetAllLessonByEducationGroupId(int id)
        {
            var educationGroup_Lesson = _educationGroup_LessonService.GetAllLessonByEducationGroupId(id);
            if (educationGroup_Lesson == null)
            {
                return NotFound();
            }
            return Ok(educationGroup_Lesson);
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationGroup_LessonReadAccess)]
        public IHttpActionResult GetAllEducationGroupByLessonId(int id)
        {
            var educationGroup_Lesson = _educationGroup_LessonService.GetAllEducationGroupByLessonId(id);
            if (educationGroup_Lesson == null)
            {
                return NotFound();
            }
            return Ok(educationGroup_Lesson);
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationGroup_LessonReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationGroup_Lesson = _educationGroup_LessonService.GetById(id);
            if (educationGroup_Lesson == null)
            {
                return NotFound();
            }
            return Ok(educationGroup_Lesson);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroup_LessonCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationGroup_LessonViewModel educationGroup_LessonViewModel)
        {
            var msgRes = _educationGroup_LessonService.Create(educationGroup_LessonViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroup_LessonUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationGroup_LessonViewModel educationGroup_LessonViewModel)
        {
            var msgRes = _educationGroup_LessonService.Update(educationGroup_LessonViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationGroup_LessonDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationGroup_LessonService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroup_LessonCreateAccess,ActionBits.EducationGroup_LessonUpdateAccess,ActionBits.EducationGroup_LessonDeleteAccess)]
        [CheckModelValidation]
        public IHttpActionResult Change(IList<EducationGroup_LessonViewModel> educationGroup_LessonViewModel)
        {
            var msgRes = _educationGroup_LessonService.Change(educationGroup_LessonViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


    }
}
