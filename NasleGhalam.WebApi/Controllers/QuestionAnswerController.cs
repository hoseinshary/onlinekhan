using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.WebApi.Extensions;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شری
	///     date: 06/02/98
	/// </author>
	public class QuestionAnswerController : ApiController
    {
        private readonly QuestionAnswerService _questionAnswerService;
        public QuestionAnswerController(QuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }

        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public HttpResponseMessage GetWordFile(string id)
        {
            var stream = new MemoryStream();
            id += ".docx";
            var filestraem = File.OpenRead(SitePath.GetQuestionAnswerAbsPath(id));
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

        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public HttpResponseMessage GetPictureFile(string id)
        {
            var stream = new MemoryStream();
            id += ".png";
            var filestraem = File.OpenRead(SitePath.GetQuestionAnswerAbsPath(id));
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


        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public IHttpActionResult GetAllByQuestionId(int id)
        {
            return Ok(_questionAnswerService.GetAllByQuestionId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionAnswer = _questionAnswerService.GetById(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }
            return Ok(questionAnswer);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult Create([FromUri]QuestionAnswerCreateViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionAnswerViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerService.Create(questionAnswerViewModel,wordFile));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        [CheckImageQuestionValidation("png")]
        public IHttpActionResult CreateForWindowsApp([FromUri]QuestionAnswerCreateViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            var pngFile = HttpContext.Current.Request.Files.Get("png");
            questionAnswerViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerService.CreateForWindowsApp(questionAnswerViewModel, wordFile , pngFile));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult CreateMulti([FromUri]QuestionAnswerCreateMultiViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionAnswerViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerService.CreateMulti(questionAnswerViewModel,wordFile));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult PreCreateMulti([FromUri]QuestionAnswerCreateMultiViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            return Ok(_questionAnswerService.PreCreateMulti(questionAnswerViewModel,wordFile));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerUpdateAccess)]
        [CheckModelValidation]
        //[CheckWordFileValidation("word", 1024)]
        public IHttpActionResult Update([FromUri]QuestionAnswerUpdateViewModel questionAnswerViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionAnswerViewModel.UserId = Request.GetUserId();
            return Ok(_questionAnswerService.Update(questionAnswerViewModel,wordFile));
        }

        [HttpPost, CheckUserAccess(ActionBits.QuestionAnswerDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionAnswerService.Delete(id));
        }
    }
}
