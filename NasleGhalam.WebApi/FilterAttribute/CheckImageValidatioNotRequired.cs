using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckImageValidatioNotRequired : ActionFilterAttribute
    {
        private readonly string _imageName;
        public CheckImageValidatioNotRequired(string imageName)
        {
            _imageName = imageName;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpPostedFile postedFile = HttpContext.Current.Request.Files.Get(_imageName);
            if (postedFile == null || postedFile.ContentLength <= 0)
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultApi
                        {
                            Message = "عکس انتخاب نشده است.",
                            MessageType = MessageType.Error
                        });
                return;
            }

            string fileExt = Path.GetExtension(postedFile.FileName);
            if (!Utility.CheckImageExtention(fileExt))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new MessageResultApi
                        {
                            Message = "فرمت عکس معتبر نمی باشد.",
                            MessageType = MessageType.Error
                        });
            }
        }
    }
}