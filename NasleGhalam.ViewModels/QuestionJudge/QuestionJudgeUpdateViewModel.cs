using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionJudge
{
    public class QuestionJudgeUpdateViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public int LookupId_QuestionHardnessType { get; set; }


        [Display(Name = "")]
        public int LookupId_RepeatnessType { get; set; }


        [Display(Name = "")]
        public byte Repeatness { get; set; }


        [Display(Name = "")]
        public bool IsStandard { get; set; }


        [Display(Name = "")]
        public bool IsDelete { get; set; }


        [Display(Name = "")]
        public bool IsUpdate { get; set; }


        [Display(Name = "")]
        public bool IsLearning { get; set; }


        [Display(Name = "")]
        public short ResponseSecond { get; set; }


        [Display(Name = "")]
        public int QuestionId { get; set; }


    }
}
