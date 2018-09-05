using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.AxillaryBook;
using NasleGhalam.WebApi.Extentions;
using NasleGhalam.Common;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 08/06/1397
	/// </author>
	public class AxillaryBookController : ApiController
    {
        private readonly AxillaryBookService _axillaryBookService;
        public AxillaryBookController(AxillaryBookService axillaryBookService)
        {
            _axillaryBookService = axillaryBookService;
        }


        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_axillaryBookService.GetAll());
        }


        [HttpGet, CheckUserAccess(ActionBits.AxillaryBookReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var axillaryBook = _axillaryBookService.GetById(id);
            if (axillaryBook == null)
            {
                return NotFound();
            }
            return Ok(axillaryBook);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.AxillaryBookCreateAccess)]
        [CheckModelValidation]
        [CheckImageValidatioNotRequired("Picture", 1024)]
        public IHttpActionResult Create(AxillaryBookViewModel axillaryBookViewModel)
        {

            var files = HttpContext.Current.Request.Files;
            var postedFile = files.Get("Picture");
            if (postedFile != null)
            {
                MessageResult message = new MessageResult();
                bool upload = false;

                //this line convert HttpPostedFile to HttpPostedFileBase
                // HttpPostedFileBase filebase = new HttpPostedFileWrapper(postedFile);
                // this line resolves a virtual path into an absolute path Example:
                //Url.Content();
                Guid g = Guid.NewGuid();
                string strextension = System.IO.Path.GetExtension(postedFile.FileName).Substring(1);
                string strPictureName = g.ToString();
                string strFullPictureName = string.Format("{0}.{1}", strPictureName, strextension);
                string strPhysicalPathName = strFullPictureName.GetAxillaryBookImagePhysicalPath();
                axillaryBookViewModel.ImgPath = strPhysicalPathName;
                axillaryBookViewModel.HasImage = true;
                message = _axillaryBookService.Create(axillaryBookViewModel);
                if (message.MessageType == MessageType.Success)
                {
                    try
                    {
                        postedFile.SaveAs(strPhysicalPathName);
                        upload = true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    //if (System.IO.File.Exists(strPhysicalPathName))
                    //{
                    //    upload = true;
                    //}
                    if (upload)
                    {
                            return Ok(new MessageResultApi
                            {
                                Message = message.FaMessage,
                                MessageType = message.MessageType,
                                Id = message.Id
                            });
                    }
                    else
                    {
                        message.FaMessage += "ولی عکس کتاب آپلود نشد.";
                        message.MessageType = MessageType.Error;
                        return Ok(new MessageResultApi
                        {
                            Message = message.FaMessage,
                            MessageType = message.MessageType,
                            Id = message.Id
                        });
                    }

                }
            }

            var msgRes = _axillaryBookService.Create(axillaryBookViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });

        }


        [HttpPost]
        [CheckUserAccess(ActionBits.AxillaryBookUpdateAccess)]
        [CheckModelValidation]
        [CheckImageValidatioNotRequired("Picture", 1024)]
        public IHttpActionResult Update(AxillaryBookViewModel axillaryBookViewModel)
        {
            var files = HttpContext.Current.Request.Files;
            var postedFile = files.Get("Picture");
            if (postedFile != null)
            {
                MessageResult message = new MessageResult();
                bool upload = false;
                Guid g = Guid.NewGuid();
                string strextension = System.IO.Path.GetExtension(postedFile.FileName).Substring(1);
                string strPictureName = g.ToString();
                string strFullPictureName = string.Format("{0}.{1}", strPictureName, strextension);
                string strPhysicalPathName = strFullPictureName.GetAxillaryBookImagePhysicalPath();
                axillaryBookViewModel.ImgPath = strPhysicalPathName;
                axillaryBookViewModel.HasImage = true;
                message = _axillaryBookService.Update(axillaryBookViewModel);
                if (message.MessageType == MessageType.Success)
                {

                    try
                    {
                        postedFile.SaveAs(strPhysicalPathName);
                        upload = true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    if (upload)
                    {
                        return Ok(new MessageResultApi
                        {
                            Message = message.FaMessage,
                            MessageType = message.MessageType
                        });
                    }
                    else
                    {
                        message.FaMessage += "ولی عکس کتاب آپلود نشد.";
                        message.MessageType = MessageType.Error;
                        return Ok(new MessageResultApi
                        {
                            Message = message.FaMessage,
                            MessageType = message.MessageType
                        });
                    }

                }
            }

            var msgRes = _axillaryBookService.Update(axillaryBookViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.AxillaryBookDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {

            var axillary = _axillaryBookService.GetById(id);
            if (axillary.HasImage)
            {
                if (System.IO.File.Exists(axillary.ImgPath))
                {
                    try
                    {
                        System.IO.File.Delete(axillary.ImgPath);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

            var msgRes = _axillaryBookService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
