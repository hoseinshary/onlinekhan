using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Tag
{
	public class TagViewModel
	{
		
		public int Id { get; set; }


		[Display(Name = "نام")]
		public string Name { get; set; }


	}
}
