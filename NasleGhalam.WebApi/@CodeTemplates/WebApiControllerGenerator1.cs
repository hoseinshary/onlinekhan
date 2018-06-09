using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationGroup;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class EducationGroupController : ApiController
	{
		private const short ReadAccess = -1;
        private const short CreateAccess = -1;
        private const short UpdateAccess = -1;
        private const short DeleteAccess = -1;

        private readonly EducationGroupService _educationGroupService;
		public EducationGroupController(EducationGroupService educationGroupService)
        {
            _educationGroupService = educationGroupService;
        }


		[HttpGet, CheckUserAccess(ActionBit = new [] { ReadAccess })]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationGroupService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBit = new [] { ReadAccess })]
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
        [CheckUserAccess(ActionBit = new [] { CreateAccess})]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationGroupViewModel educationGroupViewModel)
        {
            var msgRes = _educationGroupService.Create(educationGroupViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBit = new [] { UpdateAccess})]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationGroupViewModel educationGroupViewModel)
        {
            var msgRes = _educationGroupService.Update(educationGroupViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBit = new [] {DeleteAccess})]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationGroupService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
	}
}
