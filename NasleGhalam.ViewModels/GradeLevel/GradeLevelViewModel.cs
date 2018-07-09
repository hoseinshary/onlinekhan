using NasleGhalam.ViewModels._Attributes;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.GradeLevel
{
    public class GradeLevelViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        public byte Priority { get; set; }


        [Display(Name = "مقطع")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(Matin.Abfa.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int GradeId { get; set; }


        [Display(Name = "مقطع")]
        public string GradeName{ get; set; }

    }
}
