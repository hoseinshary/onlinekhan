using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Media;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class MediaController : ApiController
	{
        private readonly MediaService _mediaService;
		public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

		[HttpGet, CheckUserAccess(ActionBits.MediaReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_mediaService.GetAll());
        }

		[HttpGet, CheckUserAccess(ActionBits.MediaReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var media = _mediaService.GetById(id);
            if (media == null)
            {
                return NotFound();
            }
            return Ok(media);
        }

		[HttpPost]
        [CheckUserAccess(ActionBits.MediaCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(MediaCreateViewModel mediaViewModel)
        {
            return Ok(_mediaService.Create(mediaViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.MediaUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(MediaUpdateViewModel mediaViewModel)
        {
            return Ok(_mediaService.Update(mediaViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.MediaDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_mediaService.Delete(id));
        }
	}
}
