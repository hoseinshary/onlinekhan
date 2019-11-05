using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Package
{
    public class PackageUpdateViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public string Name { get; set; }


        [Display(Name = "")]
        public bool IsDelete { get; set; }


        [Display(Name = "")]
        public string ImageFile { get; set; }


        [Display(Name = "")]
        public bool IsActive { get; set; }


        [Display(Name = "")]
        public int Price { get; set; }


        [Display(Name = "")]
        public int TimeDays { get; set; }


        [Display(Name = "")]
        public DateTime CreateDateTime { get; set; }


        [Display(Name = "")]
        public string Discription { get; set; }


    }
}
