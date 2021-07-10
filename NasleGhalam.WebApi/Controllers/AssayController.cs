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
        private readonly LogService _logService;
        public AssayController(AssayService assayService, LogService logService)
        {
            _assayService = assayService;
            _logService = logService;
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

            var result = _assayService.Create(assayViewModel);
            if (result.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Assay", result.Obj, Request.GetUserId());
            }
            return Ok(result);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.AssayCreateAccess)]
        //[CheckModelValidation]
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
            var result = _assayService.Update(assayViewModel);
            if (result.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Assay", result.Obj, Request.GetUserId());
            }
            return Ok(result);
        }

        [HttpPost, CheckUserAccess(ActionBits.AssayDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var result = _assayService.Delete(id);
            if (result.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Assay", result.Obj, Request.GetUserId());
            }
            return Ok(result);
        }
    }
}
