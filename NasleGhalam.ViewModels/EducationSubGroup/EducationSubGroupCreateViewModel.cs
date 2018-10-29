using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.EducationTree;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
    public class EducationSubGroupCreateViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "درخت آموزش")]
        public int EducationTreeId { get; set; }


    }
}
