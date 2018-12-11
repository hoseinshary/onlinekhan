using System.Web.Hosting;

namespace NasleGhalam.WebApi.Extentions
{
    public static class PhysicalAddress
    {
        public static string GetQuestionPhysicalPath(this string pictureNameWithExtention)
        {
            string strRootRalativePath = "~/App_Data/Content/Question";
            string strRootRalativePathName = string.Format("{0}/{1}", strRootRalativePath, pictureNameWithExtention);
            string strPathName = HostingEnvironment.MapPath(strRootRalativePathName);
            return strPathName;
        }

        public static string GetQuestionMultiPhysicalPath(this string pictureNameWithExtention)
        {
            string strRootRalativePath = "~/App_Data/Content/Question";
            string strRootRalativePathName = string.Format("{0}/{1}", strRootRalativePath, pictureNameWithExtention);
            string strPathName = HostingEnvironment.MapPath(strRootRalativePathName);
            return strPathName;
        }

    }
}