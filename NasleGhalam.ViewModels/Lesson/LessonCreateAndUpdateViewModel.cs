using NasleGhalam.ViewModels._Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "نظام")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_Nezam { get; set; }

        [Display(Name = "پایه")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int GradeLevelId { get; set; }

        

        public int GradeId { get; set; }



        public IEnumerable<EducationGroupLessonViewModel> EducationGroups { get; set; }


    }
}
