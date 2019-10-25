using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.QuestionAnswerJudge
{
	public class QuestionAnswerJudgeViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public bool IsActiveQuestionAnswer { get; set; }


		[Display(Name = "")]
		public int QuestionAnswerId { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public string LessonName { get; set; }


		[Display(Name = "")]
		public int LookupId_ReasonProblem { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


	}
}
