using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Question
{
	public class QuestionViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Context { get; set; }


		[Display(Name = "")]
		public int QuestionNumber { get; set; }


		[Display(Name = "")]
		public int LookupId_QuestionType { get; set; }


		[Display(Name = "")]
		public int QuestionPoint { get; set; }


		[Display(Name = "")]
		public int LookupId_QuestionHardnessType { get; set; }


		[Display(Name = "")]
		public int LookupId_RepeatnessType { get; set; }


		[Display(Name = "")]
		public bool UseEvaluation { get; set; }


		[Display(Name = "")]
		public bool IsStandard { get; set; }


		[Display(Name = "")]
		public int LookupId_AuthorType { get; set; }


		[Display(Name = "")]
		public string AuthorName { get; set; }


		[Display(Name = "")]
		public short ResponseSecond { get; set; }


		[Display(Name = "")]
		public int LookupId_AreaType { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


		[Display(Name = "")]
		public string FileName { get; set; }


		[Display(Name = "")]
		public DateTime InsertDateTime { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public bool IsActive { get; set; }


	}
}
