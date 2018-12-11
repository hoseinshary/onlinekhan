using System.Web;

namespace NasleGhalam.WebApi.Util
{
    public static class SitePath
    {
        public static string AxillaryBookRelPath => "~/Content/AxillaryBook/";
        //-------------------------------------------------------------------------------------


        public static string GetAxillaryBookAbsPath(string name) => ToAbsoulutPath($"{AxillaryBookRelPath}{name}");
        //-------------------------------------------------------------------------------------


        public static string ToAbsoulutPath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }
    }
}