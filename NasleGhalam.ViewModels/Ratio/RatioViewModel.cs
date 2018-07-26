using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Ratio
{
    public class RatioViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "ضریب")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Range(0, byte.MaxValue, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Range")]
        public byte Rate { get; set; }


        [Display(Name = "درس")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int LessonId { get; set; }

        [Display(Name = "درس")]
        public string LessonName { get; set; }


        [Display(Name = "زیر گروه درسی")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int EducationSubGroupId { get; set; }

        [Display(Name = "زیر گروه درسی")]
        public string EducationSubGroupName { get; set; }


    }
}
