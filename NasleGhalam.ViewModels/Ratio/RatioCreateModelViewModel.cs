using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Ratio
{
    public class RatioCreateViewModel
    {
        [Display(Name = "گروه آموزشی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationGroupId { get; set; }

        public List<RatioExcludeLessonViewModel> RatioViewModels { get; set; }   

    }
}
