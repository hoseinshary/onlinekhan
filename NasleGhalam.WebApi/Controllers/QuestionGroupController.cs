using System;
using System.IO;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.WebApi.Extentions;
using System.Web;
using NasleGhalam.WebApi.Util;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 24-9-97
	/// </author>
	public class QuestionGroupController : ApiController
    {
        private readonly QuestionGroupService _questionGroupService;
        public QuestionGroupController(QuestionGroupService questionGroupService)
        {
            _questionGroupService = questionGroupService;
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_questionGroupService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionGroup = _questionGroupService.GetById(id);
            if (questionGroup == null)
            {
                return NotFound();
            }
            return Ok(questionGroup);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        [CheckWordFileValidation("excel", 1024)]
        public IHttpActionResult Create(QuestionGroupCreateViewModel questionGroupViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            var excelFile = HttpContext.Current.Request.Files.Get("excel");

            if (wordFile != null && wordFile.ContentLength > 0)
            {
                questionGroupViewModel.WordFile = $"{Guid.NewGuid()}{Path.GetExtension(wordFile.FileName)}";
            }

            if (excelFile != null && excelFile.ContentLength > 0)
            {
                questionGroupViewModel.ExcelFile = $"{Guid.NewGuid()}{Path.GetExtension(excelFile.FileName)}";
            }

            /////////////////////////////

            XWPFDocument document = null;
            document = new XWPFDocument(wordFile.InputStream);
            var allP = document.Paragraphs;
            //XSSFWorkbook doc2 = new XSSFWorkbook();



            //clean paragraphs
            foreach (var pragraph in allP)
            {
                if(!pragraph.IsEmpty && pragraph.Text != "" && pragraph.Text != " ")
                {
               
                }
            }




            /////////////////////////////

            questionGroupViewModel.InsertTime = DateTime.Now;
            questionGroupViewModel.UserId = Request.GetUserId();

            var msgRes = _questionGroupService.Create(questionGroupViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionGroupViewModel.WordFile) && !string.IsNullOrEmpty(questionGroupViewModel.ExcelFile))
            {
                wordFile.SaveAs(SitePath.GetQuestionAbsPath(questionGroupViewModel.WordFile));
                wordFile.SaveAs(SitePath.GetQuestionAbsPath(questionGroupViewModel.ExcelFile));
            }
            

            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            return Ok(_questionGroupService.Update(questionGroupViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_questionGroupService.Delete(id));
        }
    }
}
