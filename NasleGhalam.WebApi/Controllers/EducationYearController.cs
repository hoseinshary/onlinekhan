using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationYear;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 28/04/97
	/// </author>
	public class EducationYearController : ApiController
    {
        private readonly EducationYearService _EducationYearService;
        public EducationYearController(EducationYearService EducationYearService)
        {
            _EducationYearService = EducationYearService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationYearReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_EducationYearService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationYearReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var EducationYear = _EducationYearService.GetById(id);
            if (EducationYear == null)
            {
                return NotFound();
            }
            return Ok(EducationYear);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationYearCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationYearViewModel EducationYearViewModel)
        {
            var msgRes = _EducationYearService.Create(EducationYearViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationYearUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationYearViewModel EducationYearViewModel)
        {
            var msgRes = _EducationYearService.Update(EducationYearViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationYearDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _EducationYearService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
