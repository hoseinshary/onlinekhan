using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.EducationBook
{
    public class EducationBookViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "سال انتشار")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Range(1350, 1500, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public short PublishYear { get; set; }


        [Display(Name = "منبع کنکوری")]
        public bool IsExamSource { get; set; }


        [Display(Name = "فعال")]
        public bool IsActive { get; set; }


        [Display(Name = "تغییر نسبت به سال قبل")]
        public bool IsChanged { get; set; }


        [Display(Name = "پایه")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int GradeLevelId { get; set; }


        [Display(Name = "درس")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationGroup_LessonId { get; set; }

        public int EducationGroupId { get; set; }

        public string LessonName { get; set; }
        //public string GradeLevelName { get; set; }

        public IEnumerable<int> TopicIds { get; set; }
    }
}
