using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; }


    }
}
