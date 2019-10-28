using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.QuestionAnswerJudge
{
    public class QuestionAnswerJudgeCreateViewModel
    {
        [Display(Name = "فعال")]
        public bool IsActiveQuestionAnswer { get; set; }

        [Display(Name = "جواب سوال")]
        public int QuestionAnswerId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "درس")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string LessonName { get; set; }

        [Display(Name = "دلیل مشکل")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_ReasonProblem { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(400, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Description { get; set; }
    }
}
