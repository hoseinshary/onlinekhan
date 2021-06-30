using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Writer;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: حسین شری
    ///     date: 2015-05-15
    /// </author>
    public class WriterController : ApiController
    {
        private readonly WriterService _writerService;
        private readonly LogService _logService;
        public WriterController(WriterService writerService, LogService logService)
        {
            _writerService = writerService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.WriterReadAccess,ActionBits.WritersCodeReadAccess)]
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
            var msgRes = _writerService.Create(writerViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Writer", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.WriterUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(WriterUpdateViewModel writerViewModel)
        {
            var msgRes = _writerService.Update(writerViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Writer", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.WriterDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _writerService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Writer", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
