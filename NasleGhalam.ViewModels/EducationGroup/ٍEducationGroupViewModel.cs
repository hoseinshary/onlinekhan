using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen")]
        public string Name { get; set; }


    }
}
