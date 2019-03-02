using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.WebApi.Extentions;
using System;
using System.IO;
using NasleGhalam.WebApi.Util;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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


        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllByQuestionGroupId(int id)
        {
            return Ok(_questionService.GetAllByQuestionGroupId(id));
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


        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
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

            if (wordFile != null && wordFile.ContentLength > 0)
            {
                questionViewModel.FileName = $"{Guid.NewGuid()}";
            }

            questionViewModel.InsertDateTime = DateTime.Now;
            questionViewModel.UserId = Request.GetUserId();
            
            var msgRes = _questionService.Create(questionViewModel,wordFile);
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult Update([FromUri]QuestionUpdateViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            bool updateFile = false;
            var fileNamePrevous = "";
            if (wordFile != null && wordFile.ContentLength > 0)
            {
                fileNamePrevous = questionViewModel.FileName;
                questionViewModel.FileName = $"{Guid.NewGuid()}";
                updateFile = true;
            }

            var msgRes = _questionService.Update(questionViewModel);

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && updateFile)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(fileNamePrevous) + Path.GetExtension(wordFile.FileName)))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(fileNamePrevous) + Path.GetExtension(wordFile.FileName));
                }
                wordFile.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + Path.GetExtension(wordFile.FileName));
            }
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var question = _questionService.GetById(id);
            var msgResualt = _questionService.Delete(id);
         
            return Ok(msgResualt);
        }
    }
}
