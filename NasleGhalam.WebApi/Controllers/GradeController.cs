using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Grade;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری
	///     date: 09/04/1397
	/// </author>
	public class GradeController : ApiController
    {
        private readonly GradeService _gradeService;
        public GradeController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }


        [HttpGet, CheckUserAccess(ActionBits.GradeReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_gradeService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.GradeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var grade = _gradeService.GetById(id);
            if (grade == null)
            {
                return NotFound();
            }
            return Ok(grade);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.GradeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(GradeViewModel gradeViewModel)
        {
            var msgRes = _gradeService.Create(gradeViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.GradeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(GradeViewModel gradeViewModel)
        {
            var msgRes = _gradeService.Update(gradeViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.GradeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _gradeService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }

        [HttpGet, CheckUserAccess(ActionBits.GradeLevelCreateAccess,
            ActionBits.GradeLevelUpdateAccess,
             ActionBits.EducationBookReadAccess)]
        public IHttpActionResult GetAllDdl()
        {
            return Ok(_gradeService.GetAllDdl());
        }
    }
}
