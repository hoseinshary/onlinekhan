using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionOption
{
	public class QuestionOptionViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Context { get; set; }


		[Display(Name = "")]
		public bool IsAnswer { get; set; }


		[Display(Name = "")]
		public int QuestionId { get; set; }


	}
}
