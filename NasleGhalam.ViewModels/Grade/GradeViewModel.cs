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

        public byte Priority { get; set; }


    }
}
