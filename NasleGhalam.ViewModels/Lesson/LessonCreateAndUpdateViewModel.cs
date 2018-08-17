using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Ratio;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonCreateAndUpdateViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "اختصاصی")]
        public bool IsMain { get; set; }

        public List<EducationGroupLessonViewModel> EducationGroups { get; set; }


    }
}
