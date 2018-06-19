using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Grade
{
    public class GradeViewModel
    {
  
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        [Required]
        public byte Priority { get; set; }


    }
}
