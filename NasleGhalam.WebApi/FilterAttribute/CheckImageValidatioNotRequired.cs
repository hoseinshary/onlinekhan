using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckImageValidatioNotRequired : ActionFilterAttribute // todo: why not required?
    {
        private readonly string _imageName;
        private readonly int _imageSize;
        public CheckImageValidatioNotRequired(string imageName,int imageSize)
        {
            _imageName = imageName;
            _imageSize = imageSize;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files.Get(_imageName);
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                if (postedFile.ContentLength > (_imageSize * 1024))
                {
                    actionContext.Response = actionContext
                        .ControllerContext.Request
                        .CreateResponse(HttpStatusCode.OK,
                            new MessageResultClient
                            {
                                Message = $"عکس ارسالی باید کمتر از {_imageSize} کیلو بایت باشد.",
                                MessageType = MessageType.Error
                            });
                    return;
                }
               
            }

            if (postedFile != null && postedFile.ContentLength > 0) // todo: hashem, posted file is not null
            {
                string fileExt = Path.GetExtension(postedFile.FileName);
                if (!Utility.CheckImageExtention(fileExt))
                {
                    actionContext.Response = actionContext
                        .ControllerContext.Request
                        .CreateResponse(HttpStatusCode.OK,
                            new MessageResultClient
                            {
                                Message = "فرمت عکس معتبر نمی باشد.",
                                MessageType = MessageType.Error
                            });
                }

            }

        }
    }
}