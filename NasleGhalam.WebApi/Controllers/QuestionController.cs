using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.WebApi.Extentions;
using System.Web;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری
	///     date: 26/05/1397
	/// </author>
	public class QuestionController : ApiController
    {
        private readonly QuestionService _questionService;
        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_questionService.GetAll());
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
        public IHttpActionResult Create(QuestionCreateViewModel questionViewModel)
        {
            var files = HttpContext.Current.Request.Files;
            var wordFile = files.Get("wordFile");
            
            if (wordFile != null )
            {
                HttpPostedFileBase wordFilebase = new HttpPostedFileWrapper(wordFile);
                var resualtWord = CheckFile.UploadWordFile(wordFilebase, 1024 * 5);

                

                if (resualtWord == "OK" )
                {
                    var msgRes = _questionService.Create(questionViewModel, wordFile);
                    return Ok(new MessageResultApi
                    {
                        Message = msgRes.FaMessage,
                        MessageType = msgRes.MessageType,
                        Id = msgRes.Id
                    });
                }
                else
                {
                    return Ok(new MessageResultApi
                    {
                        Message = resualtWord 
                    });
                }
            }
            else
            {
                return Ok(new MessageResultApi
                {
                    Message = "خطای فایل!"
                });
            }


            
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult CreateMulti(QuestionTempViewModel questionViewModel)
        {
            var files = HttpContext.Current.Request.Files;
            var wordFile = files.Get("wordFile");
            var excelFile = files.Get("excelFile");



            if (wordFile != null)
            {
                HttpPostedFileBase wordFilebase = new HttpPostedFileWrapper(wordFile);
                var resualtWord = CheckFile.UploadWordFile(wordFilebase, 1024 * 5);

                HttpPostedFileBase excelFilebase = new HttpPostedFileWrapper(wordFile);
                var resualtExcel = CheckFile.UploadWordFile(excelFilebase, 1024 * 5);

                if (resualtWord == "OK" && resualtExcel == "OK")
                {
                    var msgRes = _questionService.CreateMulti(questionViewModel, wordFile, excelFile);
                    return Ok(new MessageResultApi
                    {
                        Message = msgRes.FaMessage,
                        MessageType = msgRes.MessageType,
                        Id = msgRes.Id
                    });
                }
                else
                {
                    return Ok(new MessageResultApi
                    {
                        Message = resualtWord + resualtExcel
                    });
                }
            }
            else
            {
                return Ok(new MessageResultApi
                {
                    Message = "خطای فایل!"
                });
            }



        }



        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionCreateViewModel questionViewModel)
        {
            var msgRes = _questionService.Update(questionViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _questionService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
