using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.City;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class CityController : ApiController
    {
        private readonly CityService _CityService;
        public CityController(CityService CityService)
        {
            _CityService = CityService;
        }


        [HttpGet, CheckUserAccess(ActionBits.CityReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_CityService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.CityReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var City = _CityService.GetById(id);
            if (City == null)
            {
                return NotFound();
            }
            return Ok(City);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.CityCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(CityViewModel CityViewModel)
        {
            var msgRes = _CityService.Create(CityViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.CityUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(CityViewModel CityViewModel)
        {
            var msgRes = _CityService.Update(CityViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.CityDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _CityService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
