﻿using System;
using System.IO;
using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.AxillaryBook;

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
        [CheckImageValidationNotRequired("img", 1024)]
        public IHttpActionResult Create([FromUri]AxillaryBookViewModel axillaryBookViewModel)
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                axillaryBookViewModel.ImgName = $"{Guid.NewGuid()}{Path.GetExtension(postedFile.FileName)}";
            }

            var msgRes = _axillaryBookService.Create(axillaryBookViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(axillaryBookViewModel.ImgName))
            {
                postedFile?.SaveAs(axillaryBookViewModel.ImgAbsPath);
            }

            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.AxillaryBookUpdateAccess)]
        [CheckModelValidation]
        [CheckImageValidationNotRequired("img", 1024)]
        public IHttpActionResult Update([FromUri] AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = _axillaryBookService.GetById(axillaryBookViewModel.Id);
            if (axillaryBook == null)
                return NotFound();

            var oldImgName = axillaryBook.ImgName;
            var postedFile = HttpContext.Current.Request.Files.Get("img");

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                axillaryBookViewModel.ImgName = string.IsNullOrEmpty(oldImgName) ?
                    $"{Guid.NewGuid()}{Path.GetExtension(postedFile.FileName)}" :
                    $"{Path.GetFileNameWithoutExtension(oldImgName)}{Path.GetExtension(postedFile.FileName)}";
            }

            var msgRes = _axillaryBookService.Update(axillaryBookViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(axillaryBookViewModel.ImgName))
            {
                postedFile?.SaveAs(axillaryBookViewModel.ImgAbsPath); // update image if exist
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.AxillaryBookDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _axillaryBookService.Delete(id);
            return Ok(msgRes);
        }
    }
}
