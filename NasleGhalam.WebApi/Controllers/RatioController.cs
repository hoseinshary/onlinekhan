using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Ratio;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 02/05/1397
	/// </author>
	public class RatioController : ApiController
    {
        private readonly RatioService _ratioService;
        public RatioController(RatioService ratioService)
        {
            _ratioService = ratioService;
        }


        [HttpGet, CheckUserAccess(ActionBits.RatioReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_ratioService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.RatioReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var ratio = _ratioService.GetById(id);
            if (ratio == null)
            {
                return NotFound();
            }
            return Ok(ratio);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.RatioCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(RatioViewModel ratioViewModel)
        {
            var msgRes = _ratioService.Create(ratioViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.RatioUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(RatioViewModel ratioViewModel)
        {
            var msgRes = _ratioService.Update(ratioViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.RatioDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _ratioService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
