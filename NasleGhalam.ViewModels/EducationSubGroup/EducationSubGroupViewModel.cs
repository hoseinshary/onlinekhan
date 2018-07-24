using NasleGhalam.ViewModels._Attributes;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
    public class EducationSubGroupViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "گروه تحصیلی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationGroupId { get; set; }

        [Display(Name = "گروه تحصیلی")]
        public string EducationGroupName { get; set; }

    }
}
