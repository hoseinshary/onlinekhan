using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Tag;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 11/06/1397
	/// </author>
	public class TagController : ApiController
    {
        private readonly TagService _tagService;
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet, CheckUserAccess(ActionBits.TagReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_tagService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.TagReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TagCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TagViewModel tagViewModel)
        {
            var msgRes = _tagService.Create(tagViewModel);
            return Ok(msgRes);

        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TagUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TagViewModel tagViewModel)
        {
            var msgRes = _tagService.Update(tagViewModel);
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.TagDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _tagService.Delete(id);
            return Ok(msgRes);
        }
    }
}
