using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.City;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 25/04/1397
	/// </author>
	public class CityController : ApiController
    {
        private readonly CityService _cityService;
        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet, CheckUserAccess(ActionBits.CityReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_cityService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.CityReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.CityCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(CityViewModel cityViewModel)
        {
            var msgRes = _cityService.Create(cityViewModel);
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.CityUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(CityViewModel cityViewModel)
        {
            var msgRes = _cityService.Update(cityViewModel);
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.CityDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _cityService.Delete(id);
            return Ok(msgRes);
        }

        [HttpGet, CheckUserAccess(ActionBits.UserCreateAccess,
             ActionBits.UserUpdateAccess)]
        public IHttpActionResult GetAllByProvinceIdDdl(int id)
        {
            return Ok(_cityService.GetAllByProvinceIdDdl(id));
        }
    }
}
