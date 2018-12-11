using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.GradeLevel;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری
	///     date: 09/04/1397
	/// </author>
	public class GradeLevelController : ApiController
    {
        private readonly GradeLevelService _gradeLevelService;
        public GradeLevelController(GradeLevelService gradeLevelService)
        {
            _gradeLevelService = gradeLevelService;
        }


        [HttpGet, CheckUserAccess(ActionBits.GradeLevelReadAccess,
             ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_gradeLevelService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.GradeLevelReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var gradeLevel = _gradeLevelService.GetById(id);
            if (gradeLevel == null)
            {
                return NotFound();
            }
            return Ok(gradeLevel);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.GradeLevelCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(GradeLevelViewModel gradeLevelViewModel)
        {
            var msgRes = _gradeLevelService.Create(gradeLevelViewModel);
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.GradeLevelUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(GradeLevelViewModel gradeLevelViewModel)
        {
            var msgRes = _gradeLevelService.Update(gradeLevelViewModel);
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.GradeLevelDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _gradeLevelService.Delete(id);
            return Ok(msgRes);
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationBookReadAccess)]
        public IHttpActionResult GetAllByGradeIdDdl(int id)
        {
            return Ok(_gradeLevelService.GetAllByGradeIdDdl(id));
        }
    }
}
