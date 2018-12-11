using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckExcelFileValidation : ActionFilterAttribute
    {
        private readonly string _ExcelFileName;
        private readonly int _ExcelFileSize;
        public CheckExcelFileValidation(string ExcelFileName, int ExcelFileSize)
        {
            _ExcelFileName = ExcelFileName;
            _ExcelFileSize = ExcelFileSize;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files.Get(_ExcelFileName);
            if (postedFile == null || postedFile.ContentLength <= 0)
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultClient
                        {
                            Message = $"وارد نشده است Excel فایل ",
                            MessageType = MessageType.Error
                        });
                return;
            }

            string fileExt = Path.GetExtension(postedFile.FileName);
            if (!Utility.CheckExcelFileExtention(fileExt))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultClient
                        {
                            Message = $"صحیح نمی باشد Excel فرمت فایل",
                            MessageType = MessageType.Error
                        });
            }

            if (postedFile.ContentLength > (_ExcelFileSize * 1024))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultClient
                        {
                            Message = $" عکس ارسالی باید کمتر از {_ExcelFileSize} کیلو بایت باشد.",
                            MessageType = MessageType.Error
                        });
                return;
            }
        }
    }
}