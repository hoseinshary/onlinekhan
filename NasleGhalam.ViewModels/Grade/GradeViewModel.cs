using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Grade
{
    public class GradeViewModel
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


    }
}
