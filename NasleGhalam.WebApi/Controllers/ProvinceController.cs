using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Province;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 11/4/1397
	/// </author>
	public class ProvinceController : ApiController
    {
        private readonly ProvinceService _provinceService;
        public ProvinceController(ProvinceService provinceService)
        {
            _provinceService = provinceService;
        }


        [HttpGet, CheckUserAccess(ActionBits.ProvinceReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_provinceService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.ProvinceReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var province = _provinceService.GetById(id);
            if (province == null)
            {
                return NotFound();
            }
            return Ok(province);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.ProvinceCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ProvinceViewModel provinceViewModel)
        {
            var msgRes = _provinceService.Create(provinceViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.ProvinceUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(ProvinceViewModel provinceViewModel)
        {
            var msgRes = _provinceService.Update(provinceViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.ProvinceDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _provinceService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
