using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationGroup_Lesson;
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
        // todo: دسترسی ها با شهر یکی شده! لزوم استفاده از این کنترولر چیست؟
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


        [HttpGet, CheckUserAccess(ActionBits.EducationGroup_LessonReadAccess)] // todo: correct permision
        public IHttpActionResult GetAllLessonByEducationGroupId(int id)//todo: correct name 
        {
            var educationGroup_Lesson = _educationGroup_LessonService.GetAllLessonByEducationGroupId(id);
            if (educationGroup_Lesson == null) //todo: no need null checking
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
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroup_LessonUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationGroup_LessonViewModel educationGroup_LessonViewModel)
        {
            var msgRes = _educationGroup_LessonService.Update(educationGroup_LessonViewModel);
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationGroup_LessonDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationGroup_LessonService.Delete(id);
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroup_LessonCreateAccess,ActionBits.EducationGroup_LessonUpdateAccess,ActionBits.EducationGroup_LessonDeleteAccess)]
        [CheckModelValidation]
        public IHttpActionResult Change(IList<EducationGroup_LessonViewModel> educationGroup_LessonViewModel)
        {
            var msgRes = _educationGroup_LessonService.Change(educationGroup_LessonViewModel);
            return Ok(msgRes);
        }


    }
}
