using System;
using System.Web;

namespace NasleGhalam.ServiceLayer.Util
{
    public static class SitePath
    {
        public static string AxillaryBookRelPath => "~/Content/AxillaryBook/";
        public static string QuestionRelPath => "~/Content/Question/";
        public static string QuestionGroupRelPath => "~/Content/QuestionGroup/";
        public static string QuestionGroupTempRelPath => "~/Content/QuestionGroupTemp/";
        //-------------------------------------------------------------------------------------


        public static string GetAxillaryBookAbsPath(string name) => ToAbsolutePath($"{AxillaryBookRelPath}{name}");
        //-------------------------------------------------------------------------------------


        public static string GetQuestionAbsPath(string name) => ToAbsolutePath($"{QuestionRelPath}{name}");


        public static string GetQuestionGroupAbsPath(string name) => ToAbsolutePath($"{QuestionGroupRelPath}{name}");
        public static string GetQuestionGroupTempAbsPath(string name) => ToAbsolutePath($"{QuestionGroupTempRelPath}{name}");

        public static string ToAbsolutePath(this string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }

        public static string ToFullRelativePath(this string relativePath)
        {
            var baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            return $"{baseUrl}{relativePath}";
        }
    }
}