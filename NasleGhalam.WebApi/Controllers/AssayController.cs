using System.Data.Entity.Infrastructure;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Assay;
using NasleGhalam.WebApi.Extensions;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary 
	///     date: 22/4/1398
	/// </author>
	public class AssayController : ApiController
    {
        private readonly AssayService _assayService;
        public AssayController(AssayService assayService)
        {
            _assayService = assayService;
        }

        [HttpGet, CheckUserAccess(ActionBits.AssayReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_assayService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.AssayReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var assay = _assayService.GetById(id);
            if (assay == null)
            {
                return NotFound();
            }
            return Ok(assay);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.AssayCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(AssayCreateViewModel assayViewModel)
        {
            assayViewModel.UserId = Request.GetUserId();
            return Ok(_assayService.Create(assayViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.AssayCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult GetAllQuestion(AssayCreateViewModel assayViewModel)
        {
            assayViewModel.UserId = Request.GetUserId();
            return Ok(_assayService.GetAllQuestion(assayViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.AssayCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(AssayCreateViewModel assayViewModel)
        {
            assayViewModel.UserId = Request.GetUserId();
            return Ok(_assayService.Update(assayViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.AssayDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_assayService.Delete(id));
        }
    }
}
