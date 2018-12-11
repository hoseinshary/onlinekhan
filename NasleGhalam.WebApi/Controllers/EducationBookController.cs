using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationBook;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: علیرضا اعتمادی
	///     date: 1397/06/09
	/// </author>
	public class EducationBookController : ApiController
    {
        private readonly EducationBookService _educationBookService;
        public EducationBookController(EducationBookService educationBookService)
        {
            _educationBookService = educationBookService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationBookReadAccess)]
        public IHttpActionResult GetAllByGradeLevelId(int id)
        {
            return Ok(_educationBookService.GetAllByGradeLevelId(id));
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationBookReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationBook = _educationBookService.GetById(id);
            if (educationBook == null)
            {
                return NotFound();
            }
            return Ok(educationBook);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationBookCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationBookViewModel educationBookViewModel)
        {
            var msgRes = _educationBookService.Create(educationBookViewModel);
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationBookUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationBookViewModel educationBookViewModel)
        {
            var msgRes = _educationBookService.Update(educationBookViewModel);
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationBookDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationBookService.Delete(id);
            return Ok(msgRes);
        }
    }
}
