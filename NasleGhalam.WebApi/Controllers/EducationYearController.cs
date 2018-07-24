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
        private readonly EducationYearService _educationYearService;
        public EducationYearController(EducationYearService educationYearService)
        {
            _educationYearService = educationYearService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationYearReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationYearService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationYearReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationYear = _educationYearService.GetById(id);
            if (educationYear == null)
            {
                return NotFound();
            }
            return Ok(educationYear);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationYearCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationYearViewModel educationYearViewModel)
        {
            var msgRes = _educationYearService.Create(educationYearViewModel);
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
        public IHttpActionResult Update(EducationYearViewModel educationYearViewModel)
        {
            var msgRes = _educationYearService.Update(educationYearViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationYearDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationYearService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
