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

        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_studentMajorlistService.GetAll());
        }

        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var studentMajorlist = _studentMajorlistService.GetById(id);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }

        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetStudentById()
        {
            var studentMajorlist = _studentMajorlistService.GetStudentById(Request.GetUserId(), Request.GetRoleLevel());
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }

        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetStudentMajorsById(int id)
        {
            var studentMajorlist = _studentMajorlistService.GetStudentMajorsById(id);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }


        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetStudentMajorsById()
        {
            var studentMajorlist = _studentMajorlistService.GetStudentMajorsById(Request.GetUserId());
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }


        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetAllMajors()
        {
            var studentMajorlist = _studentMajorlistService.GetAllMajors();
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }


        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        public IHttpActionResult GetMajorById(int id)
        {
            var studentMajorlist = _studentMajorlistService.GetMajorById(id);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }


        [CheckUserAccess(ActionBits.StudentMajorListReadAccess)]
        [HttpGet]
        public IHttpActionResult GetMajorsBySearch([FromUri] string text)
        {
            text =text.Replace("ی", "ي");
            text =text.Replace("ک", "ك");
            var studentMajorlist = _studentMajorlistService.GetMajorsBySearch(text);
            if (studentMajorlist == null)
            {
                return NotFound();
            }
            return Ok(studentMajorlist);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.StudentMajorListCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(StudentMajorlistViewModel studentMajorlistViewModel)
        {
            studentMajorlistViewModel.StudentId = 1;
            return Ok(_studentMajorlistService.Create(studentMajorlistViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.StudentMajorListUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(StudentMajorlistUpdateViewModel studentMajorlistViewModel)
        {
            return Ok(_studentMajorlistService.Update(studentMajorlistViewModel));
        }
        [HttpPost]
        [CheckUserAccess(ActionBits.StudentMajorListDeleteAccess)]

        public IHttpActionResult Delete(int id)
        {
            return Ok(_studentMajorlistService.Delete(id));
        }
    }
}
