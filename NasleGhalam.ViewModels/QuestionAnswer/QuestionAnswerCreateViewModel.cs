using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
    public class QuestionAnswerCreateViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نویسنده")]
        public string Author { get; set; }

        [Display(Name = "اصلی")]
        public bool IsMaster { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "سوال")]
        public int QuestionId { get; set; }

        [Display(Name = "نوع جواب")]
        public int LookupId_AnswerType { get; set; }
    }
}
