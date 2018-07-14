using NasleGhalam.ViewModels._Attributes;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationGroup_Lesson
{
    public class EducationGroup_LessonViewModel
    {
        public int? Id { get; set; }


        [Display(Name = "گروه آموزشی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(Matin.Abfa.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationGroupId { get; set; }

        [Display(Name = "گروه آموزشی")]
        public string EducatioGroupName { get; set; }



        [Display(Name = "درس")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(Matin.Abfa.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LessonId { get; set; }

        [Display(Name = "درس")]
        public string LessonName { get; set; }


        public bool IsChecked{ get; set; }

        
        
    }
}
