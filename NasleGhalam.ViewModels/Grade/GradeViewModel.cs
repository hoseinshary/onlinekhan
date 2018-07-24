using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Grade
{
    public class GradeViewModel
    {
  
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [Range(0,byte.MaxValue,ErrorMessageResourceType=typeof(ErrorResources),ErrorMessageResourceName ="Range")]
        public byte Priority { get; set; }


    }
}
