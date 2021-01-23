using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionUpdate
{
	public class QuestionUpdateViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public int QuestionId { get; set; }


		[Display(Name = "")]
		public DateTime DateTime { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


		[Display(Name = "")]
		public QuestionActivity QuestionActivity { get; set; }


	}
}
