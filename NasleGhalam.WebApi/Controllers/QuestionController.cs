using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Question;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: حسین شری
    ///     date: 22/8/97
    /// </author>
    public class QuestionController : ApiController
    {
        private readonly QuestionService _questionService;
        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByTopicIds([FromUri] IEnumerable<int> ids)
        {
            return Ok(_questionService.GetAllByTopicIds(ids));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByTopicIdsNoJudge([FromUri] IEnumerable<int> ids)
        {
            return Ok(_questionService.GetAllByTopicIdsNoJudge(ids, Request.GetUserId()));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllByQuestionGroupId(int id)
        {
            return Ok(_questionService.GetAllByQuestionGroupId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_questionService.GetAllByLessonId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var question = _questionService.GetById(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionReadAccess)]
        public HttpResponseMessage GetWordFile(string id)
        {
            var stream = new MemoryStream();
            id += ".docx";
            var filestraem = File.OpenRead(SitePath.GetQuestionAbsPath(id));
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
        //[CheckUserAccess(ActionBits.QuestionReadAccess)]
        public HttpResponseMessage GetPictureFile(string id)
        {
            var stream = new MemoryStream();
            id += ".png";
            var filestraem = File.OpenRead(SitePath.GetQuestionAbsPath(id));
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

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult Create([FromUri]QuestionCreateViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionViewModel.UserId = Request.GetUserId();

            var msgRes = _questionService.Create(questionViewModel, wordFile);
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult Update([FromUri]QuestionUpdateViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionViewModel.UserId = Request.GetUserId();

            var msgRes = _questionService.Update(questionViewModel, wordFile);
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateImportAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult UpdateImport([FromUri]QuestionUpdateImportViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");

            var msgRes = _questionService.UpdateImport(questionViewModel, wordFile);
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateTopicAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult UpdateTopic([FromUri]QuestionUpdateTopicViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");

            var msgRes = _questionService.UpdateTopic(questionViewModel, wordFile);
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgResult = _questionService.Delete(id);
            return Ok(msgResult);
        }
    }
}
