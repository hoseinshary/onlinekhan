using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationSubGroup;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 06/08/1397
	/// </author>
	public class EducationSubGroupController : ApiController
	{
        private readonly EducationSubGroupService _educationSubGroupService;
		public EducationSubGroupController(EducationSubGroupService educationSubGroupService)
        {
            _educationSubGroupService = educationSubGroupService;
        }


		[HttpGet, CheckUserAccess(ActionBits.EducationSubGroupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationSubGroupService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.EducationSubGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationSubGroup = _educationSubGroupService.GetById(id);
            if (educationSubGroup == null)
            {
                return NotFound();
            }
            return Ok(educationSubGroup);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.EducationSubGroupCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            return Ok(_educationSubGroupService.Create(educationSubGroupViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationSubGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            return Ok(_educationSubGroupService.Update(educationSubGroupViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationSubGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_educationSubGroupService.Delete(id));
        }
	}
}
