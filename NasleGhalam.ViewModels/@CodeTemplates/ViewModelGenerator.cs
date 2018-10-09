using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Student
{
	public class StudentViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string FatherName { get; set; }


		[Display(Name = "")]
		public string Address { get; set; }


	}
}
