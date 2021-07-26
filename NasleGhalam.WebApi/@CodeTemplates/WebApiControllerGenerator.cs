using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.StudentMajorlist;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class StudentMajorlistController : ApiController
	{
        private readonly StudentMajorlistService _studentMajorlistService;
		public StudentMajorlistController(StudentMajorlistService studentMajorlistService)
        {
            _studentMajorlistService = studentMajorlistService;
        }

		[HttpGet, CheckUserAccess(ActionBits.StudentMajorlistReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_studentMajorlistService.GetAll());
        }

		[HttpGet, CheckUserAccess(ActionBits.StudentMajorlistReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var studentMajorlist = _studentMajorlistService.GetById(id);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }

		[HttpPost]
        [CheckUserAccess(ActionBits.StudentMajorlistCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(StudentMajorlistCreateViewModel studentMajorlistViewModel)
        {
            return Ok(_studentMajorlistService.Create(studentMajorlistViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.StudentMajorlistUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(StudentMajorlistUpdateViewModel studentMajorlistViewModel)
        {
            return Ok(_studentMajorlistService.Update(studentMajorlistViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.StudentMajorlistDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_studentMajorlistService.Delete(id));
        }
	}
}
