using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.GradeLevel
{
    public class GradeLevelViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen")]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public byte Priority { get; set; }


        [Display(Name = "مقطع")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int GradeId { get; set; }


        [Display(Name = "مقطع")]
        public string GradeName{ get; set; }

    }
}
