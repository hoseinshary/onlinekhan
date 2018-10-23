using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationGroup;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری
	///     date: 09/04/1397
	/// </author>
	public class EducationGroupController : ApiController
    {
        private readonly EducationGroupService _educationGroupService;
        public EducationGroupController(EducationGroupService educationGroupService)
        {
            _educationGroupService = educationGroupService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationGroupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationGroupService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationGroupReadAccess)]
        public IHttpActionResult GetAllEducationWithSubGroups()
        {
            return Ok(_educationGroupService.GetAllWithSubGroups());
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationGroup = _educationGroupService.GetById(id);
            if (educationGroup == null)
            {
                return NotFound();
            }
            return Ok(educationGroup);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroupCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationGroupViewModel educationGroupViewModel)
        {
            var msgRes = _educationGroupService.Create(educationGroupViewModel);
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationGroupViewModel educationGroupViewModel)
        {
            var msgRes = _educationGroupService.Update(educationGroupViewModel);
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationGroupService.Delete(id);
            return Ok(msgRes);
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationSubGroupCreateAccess,
             ActionBits.EducationSubGroupUpdateAccess,
             ActionBits.UniversityBranchReadAccess,
             ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllDdl()
        {
            return Ok(_educationGroupService.GetAllDdl());
        }
    }
}
