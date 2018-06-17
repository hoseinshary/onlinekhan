using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationBook
{
    public class EducationBookViewModel
    {
        
        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "سال انتشار")]
        public short PublishYear { get; set; }


        [Display(Name = "منبع کنکوری")]
        public bool IsExamSource { get; set; }


        [Display(Name = "فعال")]
        public bool IsActive { get; set; }


        [Display(Name = "تغییر نسبت به سال قبل")]
        public bool IsChanged { get; set; }


        [Display(Name = "پایه")]
        public int GradeLevelId { get; set; }


        [Display(Name = "")]
        public int EducationGroup_LessonId { get; set; }


    }
}
