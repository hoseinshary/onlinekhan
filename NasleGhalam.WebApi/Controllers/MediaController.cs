using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Media;
using NasleGhalam.WebApi.Extensions;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: hosein shary
    ///     date: 1399/06/17
    /// </author>
    public class MediaController : ApiController
    {
        private readonly MediaService _mediaService;
        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }


        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionReadAccess)]
        public HttpResponseMessage GetFile(string id)
        {
            var stream = new MemoryStream();
            
            var filestraem = File.OpenRead(SitePath.GetMediaAbsPath(id));
            filestraem.CopyTo(stream);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = id
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            filestraem.Dispose();
            stream.Dispose();
            return result;
        }

        [HttpGet, CheckUserAccess(ActionBits.MediaReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_mediaService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.MediaReadAccess)]
        public IHttpActionResult GetAllByTopicIds([FromUri] IEnumerable<int> ids)
        {
            return Ok(_mediaService.GetAllByTopicIds(ids));
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
        
        public IHttpActionResult Create([FromUri]MediaCreateViewModel mediaViewModel)
        {
            var file = HttpContext.Current.Request.Files.Get("file");
            var coverImage = HttpContext.Current.Request.Files.Get("CoverImage");

            mediaViewModel.UserId = Request.GetUserId();
            
            

            return Ok(_mediaService.Create(mediaViewModel,file ,coverImage));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.MediaUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update([FromUri]MediaUpdateViewModel mediaViewModel)
        {
            var file = HttpContext.Current.Request.Files.Get("file");

            var coverImage = HttpContext.Current.Request.Files.Get("CoverImage");

           // mediaViewModel.UserId = Request.GetUserId();

            return Ok(_mediaService.Update(mediaViewModel,file,coverImage));
        }

        [HttpPost, CheckUserAccess(ActionBits.MediaDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_mediaService.Delete(id));
        }
    }
}
