using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Package;
using System.Web;
using System;
using System.IO;

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
        [CheckImageValidationNotRequired("img", 1024)]
        public IHttpActionResult Create([FromUri]PackageCreateViewModel packageViewModel)
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                packageViewModel.ImageFile = $"{Guid.NewGuid()}{Path.GetExtension(postedFile.FileName)}";
            }

            var msgRes = _packageService.Create(packageViewModel);

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(packageViewModel.ImageFile))
            {
                postedFile?.SaveAs($"{SitePath.PackageRelPath}{packageViewModel.ImageFile}".ToAbsolutePath());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PackageUpdateAccess)]
        [CheckModelValidation]
        [CheckImageValidationNotRequired("img", 1024)]
        public IHttpActionResult Update([FromUri]PackageUpdateViewModel packageViewModel)
        {

            var postedFile = HttpContext.Current.Request.Files.Get("img");
            var oldFile = packageViewModel.ImageFile;

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                packageViewModel.ImageFile = $"{Guid.NewGuid()}{Path.GetExtension(postedFile.FileName)}";
            }

            var msgRes = _packageService.Update(packageViewModel);


            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(packageViewModel.ImageFile))
            {
                if (File.Exists($"{SitePath.PackageRelPath}{oldFile}".ToAbsolutePath()))
                {
                    File.Delete($"{SitePath.PackageRelPath}{oldFile}".ToAbsolutePath());
                }
                postedFile?.SaveAs($"{SitePath.PackageRelPath}{packageViewModel.ImageFile}".ToAbsolutePath());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.PackageDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_packageService.Delete(id));
        }
    }
}
