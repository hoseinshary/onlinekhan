using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Publisher;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 07/06/1397
	/// </author>
	public class PublisherController : ApiController
    {
        private readonly PublisherService _publisherService;
        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }


        [HttpGet, CheckUserAccess(ActionBits.PublisherReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_publisherService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.PublisherReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var publisher = _publisherService.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.PublisherCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(PublisherViewModel publisherViewModel)
        {
            var msgRes = _publisherService.Create(publisherViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.PublisherUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(PublisherViewModel publisherViewModel)
        {
            var msgRes = _publisherService.Update(publisherViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.PublisherDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _publisherService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
