using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }

        [Display(Name = "اختصاصی")]
        public bool IsMain { get; set; }
    }
}
