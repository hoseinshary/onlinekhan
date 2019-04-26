using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionAnswer
{
	public class QuestionAnswerViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Title { get; set; }


		[Display(Name = "")]
		public string Context { get; set; }


		[Display(Name = "")]
		public string FilePath { get; set; }


		[Display(Name = "")]
		public int LookupId_AnswerType { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


		[Display(Name = "")]
		public string Author { get; set; }


		[Display(Name = "")]
		public bool IsMaster { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public int QuestionId { get; set; }


	}
}
