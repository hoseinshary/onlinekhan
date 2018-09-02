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
        public IHttpActionResult Create(AxillaryBookViewModel axillaryBookViewModel)
        {
           
            var files = HttpContext.Current.Request.Files;
            var postedFile = files.Get("Picture");
            if (postedFile != null)
            {
                MessageResult message = new MessageResult();
                const int picSize = 1024;
                bool upload = false;
                string picUpload = null;
                
                HttpPostedFileBase filebase = new HttpPostedFileWrapper(postedFile);
                picUpload = CheckFile.UploadPictureFile(filebase, picSize);
                
                if (picUpload.Equals("Check Picture Is Successfully"))
                {
                    //Url.Content();
                    string strextension = System.IO.Path.GetExtension(postedFile.FileName).Substring(1);
                    string strPictureName = axillaryBookViewModel.Name;
                    string strFullPictureName = string.Format("{0}.{1}", strPictureName, strextension);
                    string strPhysicalPathName = strFullPictureName.GetAxillaryBookImagePhysicalPath();
                    axillaryBookViewModel.ImgPath = strPhysicalPathName;
                    message = _axillaryBookService.Create(axillaryBookViewModel);
                    if (message.MessageType == MessageType.Success)
                    {
                        
                        try
                        {
                            postedFile.SaveAs(strPhysicalPathName);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        if(System.IO.File.Exists(strPhysicalPathName))
                        {
                            upload = true;
                        }
                        if(upload)
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
                else if (picUpload.Equals("File Extension Is InValid - Only Upload jpg/jpeg/png File"))
                {
                    return Ok(new MessageResultApi
                    {
                        Message = "قابل قبول هستند" + "jpg/jpeg/png" + "پسوند فایل ارسالی اشتباه است.فقط فایل هایی با پسوند",
                        MessageType = MessageType.Error
                    });
                }
                else if (picUpload.Equals("File size Should Be UpTo " + picSize.ToString() + "KB"))
                {
                    return Ok(new MessageResultApi
                    {
                        //Message = "باشد" + "KB" + picSize.ToString() + "عکس ارسالی باید کمتر از",
                        Message = $"عکس ارسالی باید کمتر از {picSize} کیلو بایت باشد.",
                        MessageType = MessageType.Error
                    });
                }
                else
                {
                    return Ok(new MessageResultApi
                    {
                        Message = "فایل عکس خوانا نمی باشد",
                        MessageType = MessageType.Error
                    });
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
        public IHttpActionResult Update(AxillaryBookViewModel axillaryBookViewModel)
        {
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
            var msgRes = _axillaryBookService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
    }
}
