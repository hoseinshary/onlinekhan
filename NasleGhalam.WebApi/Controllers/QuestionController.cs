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
                //{Path.GetExtension(wordFile.FileName)}
                questionViewModel.FileName = $"{Guid.NewGuid()}";
            }
            /////////////////////////////


            MemoryStream tempStream = new MemoryStream();
            wordFile.InputStream.CopyTo(tempStream);
            wordFile.InputStream.Position = tempStream.Position = 0;
            var document = new XWPFDocument(tempStream);
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

            ////get options
            //int optionNumbers = 0;
            //var optionArray  = new List<QuestionOptionViewModel>();
            //for(int i = allP.Count-1; optionNumbers < 4 ; i--)
            //{
            //    if (!allP[i].IsEmpty && allP[i].Text != "" && allP[i].Text != " ")
            //    {
            //        optionNumbers++;
            //        QuestionOptionViewModel newOption = new QuestionOptionViewModel();
            //        newOption.Context = allP[i].Text;
            //        if ((questionViewModel.AnswerNumber == 4 && optionNumbers == 1) ||
            //            (questionViewModel.AnswerNumber == 3 && optionNumbers == 2) ||
            //            (questionViewModel.AnswerNumber == 2 && optionNumbers == 3) ||
            //            (questionViewModel.AnswerNumber == 1 && optionNumbers == 4))
            //            newOption.IsAnswer = true;
            //        else
            //            newOption.IsAnswer = false;

            //        optionArray.Add(newOption);

            //    }
            //}

            //for (int i = optionArray.Count-1 ; i >= 0; i--)
            //{
            //    questionViewModel.Options.Add(optionArray[i]);
            //}


            questionViewModel.InsertDateTime = DateTime.Now;
            questionViewModel.UserId = Request.GetUserId();

            /////////////////////////////////
            var msgRes = _questionService.Create(questionViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName))
            {
                wordFile.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName + Path.GetExtension(wordFile.FileName)));
            }

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
            if (msgResualt.MessageType == MessageType.Success)
            {
                File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".docx");
            }
            return Ok(msgResualt);
        }
    }
}
