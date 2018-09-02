using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace NasleGhalam.WebApi.Extentions
{
    public static class PhysicalAddress
    {
        public static string GetQuestionPhysicalPath(this string pictureNameWithExtention)
        {
            string strRootRalativePath = "~/App_Data/Content/QuestionWordFile";
            string strRootRalativePathName = string.Format("{0}/{1}", strRootRalativePath, pictureNameWithExtention);
            string strPathName = HostingEnvironment.MapPath(strRootRalativePathName);
            return strPathName;
        }

        public static string GetQuestionMultiPhysicalPath(this string pictureNameWithExtention)
        {
            string strRootRalativePath = "~/App_Data/Content/QuestionWordFile/Multi";
            string strRootRalativePathName = string.Format("{0}/{1}", strRootRalativePath, pictureNameWithExtention);
            string strPathName = HostingEnvironment.MapPath(strRootRalativePathName);
            return strPathName;
        }

    }
}