using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionAnswerJudge
{
    public class QuestionAnswerJudgeCreateViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "فعال")]
        public bool IsActiveQuestionAnswer { get; set; }


        [Display(Name = "جواب سوال")]
        public int QuestionAnswerId { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "درس")]
        public string LessonName { get; set; }


        [Display(Name = "دلیل مشکل")]
        public int LookupId_ReasonProblem { get; set; }


        [Display(Name = "توضیحات")]
        public string Description { get; set; }


    }
}
