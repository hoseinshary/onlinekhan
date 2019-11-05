using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Package
{
    public class PackageCreateViewModel
    {
        

        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "تصویر")]
        public string ImageFile { get; set; }


        [Display(Name = "فعال")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public bool IsActive { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int Price { get; set; }


        [Display(Name = "مدت به روز")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int TimeDays { get; set; }


        [Display(Name = "تاریخ ساخت")]
        public DateTime CreateDateTime { get; set; }


        [Display(Name = "توضیحات")]
        public string Discription { get; set; }


        [Display(Name = "درس ها")]
        public ICollection<int> LessonIds { get; set; }


    }
}
