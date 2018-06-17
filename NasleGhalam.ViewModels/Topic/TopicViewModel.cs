using NasleGhalam.Common;
using System.ComponentModel.DataAnnotations;


namespace NasleGhalam.ViewModels.Topic
{
    class TopicViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "سهمیه در کنکور")]
        public int ExamStock { get; set; }

        [Display(Name = "سهمیه در  سیستمی")]
        public int ExamStockSystem { get; set; }

        [Display(Name = "ضریب اهمیت")]
        public short Importance { get; set; }

        [Display(Name = "کنکوری")]
        public bool IsExamSource { get; set; }

        [Display(Name = "")]
        public TopicHardnessType HardnessType { get; set; }

        [Display(Name = "")]
        public AreaType AreaType { get; set; }

        [Display(Name = "")]
        public bool IsActive { get; set; }

        [Display(Name = "")]
        public int? ParentTopicId { get; set; }
        

        

        [Display(Name = "")]
        public int EducationGroup_LessonId { get; set; }

    }
}
