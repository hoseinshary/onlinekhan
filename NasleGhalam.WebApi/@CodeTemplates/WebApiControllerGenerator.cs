using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Resume;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class ResumeController : ApiController
	{
        private readonly ResumeService _resumeService;
		public ResumeController(ResumeService resumeService)
        {
            _resumeService = resumeService;
        }

		[HttpGet, CheckUserAccess(ActionBits.ResumeReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_resumeService.GetAll());
        }

		[HttpGet, CheckUserAccess(ActionBits.ResumeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var resume = _resumeService.GetById(id);
            if (resume == null)
            {
                return NotFound();
            }
            return Ok(resume);
        }

		[HttpPost]
        [CheckUserAccess(ActionBits.ResumeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ResumeCreateViewModel resumeViewModel)
        {
            return Ok(_resumeService.Create(resumeViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ResumeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(ResumeUpdateViewModel resumeViewModel)
        {
            return Ok(_resumeService.Update(resumeViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.ResumeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_resumeService.Delete(id));
        }
	}
}
