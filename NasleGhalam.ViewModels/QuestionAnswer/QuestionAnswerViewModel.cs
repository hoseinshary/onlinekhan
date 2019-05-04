using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }


        [Display(Name = "عنوان")]
        public string Title { get; set; }


        [Display(Name = "متن")]
        public string Context { get; set; }


        [Display(Name = "آدرس فایل")]
        public string FilePath { get; set; }


        public string QuestionAnswerPicturePath => $"/Api/QuestionAnswer/GetPictureFile/{FilePath}".ToFullRelativePath();

        [Display(Name = "نوع پاسخ")]
        public int LookupId_AnswerType { get; set; }


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
    }
}
