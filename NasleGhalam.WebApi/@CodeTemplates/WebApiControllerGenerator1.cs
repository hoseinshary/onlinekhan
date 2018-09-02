using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.AxillaryBook;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 08/06/1397
	/// </author>
	public class AxillaryBookController : ApiController
	{
        private readonly AxillaryBookService _axillaryBookService;
		public AxillaryBookController(AxillaryBookService axillaryBookService)
        {
            _axillaryBookService = axillaryBookService;
        }


		[HttpGet, CheckUserAccess(ActionBits.AxillaryBookReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_axillaryBookService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.AxillaryBookReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var axillaryBook = _axillaryBookService.GetById(id);
            if (axillaryBook == null)
            {
                return NotFound();
            }
            return Ok(axillaryBook);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.AxillaryBookCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(AxillaryBookViewModel axillaryBookViewModel)
        {
            var msgRes = _axillaryBookService.Create(axillaryBookViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.AxillaryBookUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(AxillaryBookViewModel axillaryBookViewModel)
        {
            var msgRes = _axillaryBookService.Update(axillaryBookViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.AxillaryBookDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _axillaryBookService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
	}
}
