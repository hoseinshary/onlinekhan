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
using NPOI.XWPF.UserModel;
using NasleGhalam.ViewModels.QuestionOption;

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
            var filestraem = File.OpenRead(SitePath.GetQuestionAbsPath(id));
            filestraem.CopyTo(stream);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = id
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

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
                questionViewModel.FileName = $"{Guid.NewGuid()}{Path.GetExtension(wordFile.FileName)}";
            }
            /////////////////////////////

            
            
            XWPFDocument document = null;
            document = new XWPFDocument(wordFile.InputStream);
            var allP = document.Paragraphs;
            
            
            
            

            //clean paragraphs
            foreach (var pragraph in allP)
            {
                //pragraph.
                ////if(!pragraph.IsEmpty && pragraph.Text != "" && pragraph.Text != " ")
                //{
                    questionViewModel.Context += pragraph.Text;
                //}
            }

            //get options
            int optionNumbers = 0;
            var optionArray  = new List<QuestionOptionViewModel>();
            for(int i = allP.Count-1; optionNumbers < 4 ; i--)
            {
                if (!allP[i].IsEmpty && allP[i].Text != "" && allP[i].Text != " ")
                {
                    optionNumbers++;
                    QuestionOptionViewModel newOption = new QuestionOptionViewModel();
                    newOption.Context = allP[i].Text;
                    if ((questionViewModel.AnswerNumber == 4 && optionNumbers == 1) ||
                        (questionViewModel.AnswerNumber == 3 && optionNumbers == 2) ||
                        (questionViewModel.AnswerNumber == 2 && optionNumbers == 3) ||
                        (questionViewModel.AnswerNumber == 1 && optionNumbers == 4))
                        newOption.IsAnswer = true;
                    else
                        newOption.IsAnswer = false;

                    optionArray.Add(newOption);

                }
            }

            for (int i = optionArray.Count-1 ; i >= 0; i--)
            {
                questionViewModel.Options.Add(optionArray[i]);
            }


            questionViewModel.InsertDateTime = DateTime.Now;
            questionViewModel.UserId = Request.GetUserId();

            /////////////////////////////////
            var msgRes = _questionService.Create(questionViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName))
            {
                wordFile.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName));
            }

            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult Update(QuestionUpdateViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            bool updateFile = false;
            var fileNamePrevous = "";
            if (wordFile != null && wordFile.ContentLength > 0)
            {
                fileNamePrevous = questionViewModel.FileName;
                questionViewModel.FileName = $"{Guid.NewGuid()}{Path.GetExtension(wordFile.FileName)}";
                updateFile = true;
            }

            var msgRes = _questionService.Update(questionViewModel);

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && updateFile)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(fileNamePrevous)))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(fileNamePrevous));
                }
                wordFile.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName));
            }
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionService.Delete(id));
        }
    }
}
