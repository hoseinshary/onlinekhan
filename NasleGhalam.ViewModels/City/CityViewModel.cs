using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.City
{
    public class CityViewModel
    {
        
        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "استان")]
        public int ProvinceId { get; set; }


    }
}
