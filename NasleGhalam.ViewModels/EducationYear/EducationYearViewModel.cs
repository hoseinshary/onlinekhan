using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationYear
{
    public class EducationYearViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "سال جاری")]
        public bool IsActiveYear { get; set; }


    }
}
