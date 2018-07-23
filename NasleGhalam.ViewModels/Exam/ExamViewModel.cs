using NasleGhalam.ViewModels._Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Exam
{
    public class ExamViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }

        //TODO: complate data anotation
        [Display(Name = "تاریخ امتحان")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public DateTime Date { get; set; }

        [Display(Name = "گروه درسی")]
        public string EducationGroupName { get; set; }

        [Display(Name = "گروه درسی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationGroupId { get; set; }

        [Display(Name = "سال تحصیلی")]
        public string EducationYearName { get; set; }

        [Display(Name = "سال تحصیلی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationYearId { get; set; }


    }
}
