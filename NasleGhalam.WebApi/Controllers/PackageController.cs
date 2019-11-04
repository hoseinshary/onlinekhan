using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Package;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hoseinshary   
	///     date: 13/8/98
	/// </author>
	public class PackageController : ApiController
    {
        private readonly PackageService _packageService;
        public PackageController(PackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet, CheckUserAccess(ActionBits.PackageReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_packageService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.PackageReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var package = _packageService.GetById(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PackageCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(PackageCreateViewModel packageViewModel)
        {
            return Ok(_packageService.Create(packageViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PackageUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(PackageUpdateViewModel packageViewModel)
        {
            return Ok(_packageService.Update(packageViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.PackageDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_packageService.Delete(id));
        }
    }
}
