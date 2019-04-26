using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
    public class QuestionAnswerCreateMultiViewModel
    {
 

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        
        
        

        [Display(Name = "نویسنده")]
        public string Author { get; set; }

        

        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "سوال گروهی")]
        public int QuestionGroupId { get; set; }


    }
}
