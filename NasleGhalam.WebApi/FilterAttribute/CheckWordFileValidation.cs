using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckWordFileValidation : ActionFilterAttribute
    {
        private readonly string _WordFileName;
        private readonly int _WordFileSize;
        public CheckWordFileValidation(string wordFileName, int wordFileSize)
        {
            _WordFileName = wordFileName;
            _WordFileSize = wordFileSize;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files.Get(_WordFileName);
            if (postedFile == null || postedFile.ContentLength <= 0)
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultClient
                        {
                            Message = $"وارد نشده است Word فایل ",
                            MessageType = MessageType.Error
                        });
                return;
            }

            string fileExt = Path.GetExtension(postedFile.FileName);
            if (!Utility.CheckWordFileExtention(fileExt))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultClient
                        {
                            Message = $"صحیح نمی باشد word فرمت فایل",
                            MessageType = MessageType.Error
                        });
            }

            if (postedFile.ContentLength > (_WordFileSize * 1024)) // todo: check length necessary?
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultClient
                        {
                            Message = $" عکس ارسالی باید کمتر از {_WordFileSize} کیلو بایت باشد.", // todo: عکس :D
                            MessageType = MessageType.Error
                        });
            }
        }
    }
}