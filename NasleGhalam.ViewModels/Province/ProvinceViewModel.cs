using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Province
{
    public class ProvinceViewModel
    {
   
        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "پیش شماره")]
        public string Code { get; set; }


    }
}
