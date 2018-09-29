using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.UniversityBranch;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 17/06/1397
	/// </author>
	public class UniversityBranchController : ApiController
    {
        private readonly UniversityBranchService _universityBranchService;
        public UniversityBranchController(UniversityBranchService universityBranchService)
        {
            _universityBranchService = universityBranchService;
        }


        [HttpGet, CheckUserAccess(ActionBits.UniversityBranchReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_universityBranchService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.UniversityBranchReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var universityBranch = _universityBranchService.GetById(id);
            if (universityBranch == null)
            {
                return NotFound();
            }
            return Ok(universityBranch);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.UniversityBranchCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(UniversityBranchViewModel universityBranchViewModel)
        {
            var msgRes = _universityBranchService.Create(universityBranchViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.UniversityBranchUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(UniversityBranchViewModel universityBranchViewModel)
        {
            var msgRes = _universityBranchService.Update(universityBranchViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.UniversityBranchDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _universityBranchService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
