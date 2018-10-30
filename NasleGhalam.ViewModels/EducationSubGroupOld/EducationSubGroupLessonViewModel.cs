using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
    public class EducationSubGroupLessonViewModel
    {

        public int EducationSubGroupId { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        public byte Rate { get; set; }


    }
}
