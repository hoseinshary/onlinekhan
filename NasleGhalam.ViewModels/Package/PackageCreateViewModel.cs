using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Package
{
    public class PackageCreateViewModel
    {
        

        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


        [Display(Name = "")]
        public bool IsDelete { get; set; }


        [Display(Name = "تصویر")]
        public string ImageFile { get; set; }


        [Display(Name = "فعال")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public bool IsActive { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public int Price { get; set; }


        [Display(Name = "تاریخ مصرف")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public System.DateTime Expire { get; set; }


        [Display(Name = "تاریخ ساخت")]
        public DateTime CreateDateTime { get; set; }


        [Display(Name = "توضیحات")]
        public string Discription { get; set; }


    }
}
