using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Writer;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class WriterController : ApiController
	{
        private readonly WriterService _writerService;
		public WriterController(WriterService writerService)
        {
            _writerService = writerService;
        }


		[HttpGet, CheckUserAccess(ActionBits.WriterReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_writerService.GetAll());
        }


		[HttpGet, CheckUserAccess(ActionBits.WriterReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var writer = _writerService.GetById(id);
            if (writer == null)
            {
                return NotFound();
            }
            return Ok(writer);
        }


		[HttpPost]
        [CheckUserAccess(ActionBits.WriterCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(WriterCreateViewModel writerViewModel)
        {
            return Ok(_writerService.Create(writerViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.WriterUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(WriterUpdateViewModel writerViewModel)
        {
            return Ok(_writerService.Update(writerViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.WriterDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_writerService.Delete(id));
        }
	}
}
