using System;
using System.Web;

namespace NasleGhalam.Common
{
    public static class SitePath
    {
        public static string AxillaryBookRelPath => "/Content/AxillaryBook/";
        public static string QuestionRelPath => "~/Content/Question/";
        public static string QuestionOptionsRelPath => "~/Content/QuestionOptions/";
        public static string QuestionAnswerRelPath => "~/Content/QuestionAnswer/";
        public static string QuestionGroupRelPath => "~/Content/QuestionGroup/";
        public static string QuestionGroupTempRelPath => "~/Content/QuestionGroupTemp/";

        public static string UserProfileRelPath => "~/Content/UserProfile/";
        //-------------------------------------------------------------------------------------

        public static string GetQuestionAbsPath(string name) => ToAbsolutePath($"{QuestionRelPath}{name}");

        public static string GetQuestionOptionsAbsPath(string name) => ToAbsolutePath($"{QuestionOptionsRelPath}{name}");

        public static string GetQuestionAnswerAbsPath(string name) => ToAbsolutePath($"{QuestionAnswerRelPath}{name}");

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