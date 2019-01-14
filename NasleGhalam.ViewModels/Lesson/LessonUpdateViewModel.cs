using NasleGhalam.ViewModels._Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }

        [Display(Name = "اختصاصی")]
        public bool IsMain { get; set; }

        [Display(Name = "نظام")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_Nezam { get; set; }

        public IEnumerable<RatioUpdateViewModel> Ratios { get; set; }

        public IEnumerable<int> EducationTreeIds { get; set; }
    }
}
