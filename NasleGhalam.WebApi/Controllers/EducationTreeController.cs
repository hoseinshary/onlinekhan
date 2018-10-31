using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 06/08/97
	/// </author>
	public class EducationTreeController : ApiController
    {
        private readonly EducationTreeService _educationTreeService;
        public EducationTreeController(EducationTreeService educationTreeService)
        {
            _educationTreeService = educationTreeService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationTreeService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess)]
        public IHttpActionResult GetChildren(int id )
        {
            return Ok(_educationTreeService.GetChildren(id));
        }



        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess)]
        public IHttpActionResult GetAllEducationTreeByState(EducationTreeState state)
        {
            return Ok(_educationTreeService.GetAllEducationTreeByState(state));
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess)]
        public IHttpActionResult GetAllByLookupId(int id)
        {
            return Ok(_educationTreeService.GetAllByLookupId(id));
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationTree = _educationTreeService.GetById(id);
            if (educationTree == null)
            {
                return NotFound();
            }
            return Ok(educationTree);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationTreeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationTreeCreateViewModel educationTreeViewModel)
        {
            return Ok(_educationTreeService.Create(educationTreeViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationTreeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationTreeUpdateViewModel educationTreeViewModel)
        {
            return Ok(_educationTreeService.Update(educationTreeViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationTreeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_educationTreeService.Delete(id));
        }
    }
}
