using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Assay
{
    public class AssayCreateViewModel
    {
        public IEnumerable<LessonAssayViewModel> Lessons { get; set; }

        public bool RandomQuestion { get; set; }

        public bool RandomOptions { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(75, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Title { get; set; }


        [Display(Name = "زمان")]
        public int Time { get; set; }


        [Display(Name = "اهمیت")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int LookupId_Importance { get; set; }


        [Display(Name = "نوع")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int LookupId_Type { get; set; }


        [Display(Name = "نوع سوالات")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int LookupId_QuestionType { get; set; }


        [Display(Name = "قابل اجرا برای همه")]
        public bool IsPublic { get; set; }


        [Display(Name = "آنلاین")]
        public bool IsOnline { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }


        [Display(Name = "تاریخ ثبت")]
        public DateTime DateTimeCreate { get; set; }


        [Display(Name = "فایل")]
        public string File { get; set; }

    }
}
