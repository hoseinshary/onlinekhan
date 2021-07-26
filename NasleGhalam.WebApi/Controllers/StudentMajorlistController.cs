using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.StudentMajorlist;
using NasleGhalam.WebApi.Extensions;

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

        public IHttpActionResult GetAll()
        {
            return Ok(_studentMajorlistService.GetAll());
        }

        public IHttpActionResult GetById(int id)
        {
            var studentMajorlist = _studentMajorlistService.GetById(id);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }
        public IHttpActionResult GetByStudentId(int id)
        {
            var studentMajorlist = _studentMajorlistService.GetStudentById(id);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }
        
        [HttpPost]
        [CheckModelValidation]
        public IHttpActionResult Create(StudentMajorlistViewModel studentMajorlistViewModel)
        {
            studentMajorlistViewModel.StudentId = 1;
            return Ok(_studentMajorlistService.Create(studentMajorlistViewModel));
        }

        [HttpPost]
        [CheckModelValidation]
        public IHttpActionResult Update(StudentMajorlistUpdateViewModel studentMajorlistViewModel)
        {
            return Ok(_studentMajorlistService.Update(studentMajorlistViewModel));
        }
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_studentMajorlistService.Delete(id));
        }
    }
}
