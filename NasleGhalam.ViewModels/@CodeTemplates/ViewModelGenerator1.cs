using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.City
{
	public class CityViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public int ProvinceId { get; set; }


	}
}
