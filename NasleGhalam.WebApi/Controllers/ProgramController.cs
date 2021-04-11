using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Program;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 19/6/2020
	/// </author>
	public class ProgramController : ApiController
    {
        private readonly ProgramService _programService;
        public ProgramController(ProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet, CheckUserAccess(ActionBits.ProgramReadAccess)]
        public IHttpActionResult GetAll()
        {
            int userid = Request.GetUserId();
            return Ok(_programService.GetAll(userid));
        }

        [HttpGet, CheckUserAccess(ActionBits.ProgramReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var program = _programService.GetById(id);
            if (program == null)
            {
                return NotFound();
            }
            return Ok(program);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ProgramCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ProgramCreateViewModel programViewModel)
        {
            return Ok(_programService.Create(programViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ProgramUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(ProgramCreateViewModel programViewModel)
        {
            return Ok(_programService.Update(programViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.ProgramDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_programService.Delete(id));
        }
    }
}
