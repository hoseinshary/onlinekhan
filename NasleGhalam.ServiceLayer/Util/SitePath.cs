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


        public static string GetAxillaryBookAbsPath(string name) => ToAbsoulutPath($"{AxillaryBookRelPath}{name}");
        //-------------------------------------------------------------------------------------


        public static string GetQuestionAbsPath(string name) => ToAbsoulutPath($"{QuestionRelPath}{name}");


        public static string GetQuestionGroupAbsPath(string name) => ToAbsoulutPath($"{QuestionGroupRelPath}{name}");
        public static string GetQuestionGroupTempAbsPath(string name) => ToAbsoulutPath($"{QuestionGroupTempRelPath}{name}");

        public static string ToAbsoulutPath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }
    }
}