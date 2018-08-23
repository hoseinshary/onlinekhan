using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 29/05/1397
	/// </author>
	public class LookupController : ApiController
	{
        private readonly LookupService _lookupService;
		public LookupController(LookupService lookupService)
        {
            _lookupService = lookupService;
        }


		[HttpGet, CheckUserAccess(ActionBits.LookupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_lookupService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.LookupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var lookup = _lookupService.GetById(id);
            if (lookup == null)
            {
                return NotFound();
            }
            return Ok(lookup);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.LookupCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(LookupViewModel lookupViewModel)
        {
            var msgRes = _lookupService.Create(lookupViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.LookupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(LookupViewModel lookupViewModel)
        {
            var msgRes = _lookupService.Update(lookupViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.LookupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _lookupService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
	}
}
