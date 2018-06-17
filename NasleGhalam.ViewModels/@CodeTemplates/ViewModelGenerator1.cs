using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Grade
{
	public class GradeViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public byte Priority { get; set; }


	}
}
