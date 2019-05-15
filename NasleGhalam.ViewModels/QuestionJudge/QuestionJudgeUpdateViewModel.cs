using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionJudge
{
    public class QuestionJudgeUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "درجه سختی")]
        public int LookupId_QuestionHardnessType { get; set; }

        [Display(Name = "درجه تکرار")]
        public int LookupId_RepeatnessType { get; set; }

        [Display(Name = "استاندارد")]
        public bool IsStandard { get; set; }

        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }

        [Display(Name = "ویرایش")]
        public bool IsUpdate { get; set; }

        [Display(Name = "تایید")]
        public bool IsActiveQuestion { get; set; }

        [Display(Name = "تایید جواب")]
        public bool IsActiveQuestionAnswer { get; set; }

        [Display(Name = "یادگیری")]
        public bool IsLearning { get; set; }

        [Display(Name = "زمان پاسخ")]
        public short ResponseSecond { get; set; }

        [Display(Name = "سوال")]
        public int QuestionId { get; set; }
    }
}
