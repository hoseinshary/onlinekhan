using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace NasleGhalam.Common
{
    public class CheckPicForUplode
    {
        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }
        public string UploadUserFile(HttpPostedFileBase file)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
              //var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" }; 
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ErrorMessage = "File Extension Is InValid - Only Upload jpg/jpeg/png File";
                    return ErrorMessage;
                }
                else if (file.ContentLength > (filesize * 1024))
                {
                    ErrorMessage = "File size Should Be UpTo " + filesize + "KB";
                    return ErrorMessage;
                }
                else
                {
                    ErrorMessage = "Check Picture Is Successfully";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Check Picture Error";
                return ErrorMessage;
            }
        }
    }
}

