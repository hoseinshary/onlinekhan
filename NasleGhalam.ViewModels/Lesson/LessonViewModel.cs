using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required]
        [StringLength(50 , MinimumLength =4)]
        public string Name { get; set; }

        
        [Display(Name = "اختصاصی")]
        [Required]
        public bool IsMain { get; set; }
    }
}
