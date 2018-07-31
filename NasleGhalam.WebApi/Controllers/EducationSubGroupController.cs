using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 01/05/1397
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
            var msgRes = _educationSubGroupService.Create(educationSubGroupViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationSubGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationSubGroupViewModel educationSubGroupViewModel)
        {
            var msgRes = _educationSubGroupService.Update(educationSubGroupViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationSubGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationSubGroupService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
