using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace NasleGhalam.WebApi.Extentions
{
    public static class PhysicalAddress
    {
        public static string GetAxillaryBookImagePhysicalPath(this string pictureNameWithExtention)
        {
            string strRootRalativePath = "~/App_Data/Content/AxillaryBooksPictures";
            string strRootRalativePathName = string.Format("{0}/{1}", strRootRalativePath, pictureNameWithExtention);
            string strPathName = HostingEnvironment.MapPath(strRootRalativePathName);
            return strPathName;
        }
    }
}