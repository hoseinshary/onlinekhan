using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.GradeLevel
{
    public class GradeLevelViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        [Required]
        public byte Priority { get; set; }


        [Display(Name = "مقطع")]
        [Required]
        public int GradeId { get; set; }


        [Display(Name = "مقطع")]
        public string GradeName{ get; set; }

    }
}
