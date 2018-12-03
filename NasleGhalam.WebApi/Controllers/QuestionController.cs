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
        public IHttpActionResult GetAll([FromUri] IEnumerable<int> ids)
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
            XWPFDocument doc2 = new XWPFDocument();
            
            //clean paragraphs
            foreach (var pragraph in allP)
            {
                //if(!pragraph.IsEmpty && pragraph.Text != "" && pragraph.Text != " ")
                {
                    questionViewModel.Context += pragraph.Text;
                }
            }

            //get options
            int optionNumbers = 0;
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

                    questionViewModel.Options.Add(newOption);

                }
            }


            /////////////////////////////////
            var msgRes = _questionService.Create(questionViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName))
            {
                wordFile.SaveAs(SitePath.GetAxillaryBookAbsPath(questionViewModel.FileName));
            }

            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionUpdateViewModel questionViewModel)
        {
            return Ok(_questionService.Update(questionViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionService.Delete(id));
        }
    }
}
