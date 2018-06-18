using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.GradeLevel
{
	public class GradeLevelViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Context { get; set; }


		[Display(Name = "")]
		public int QuestionNumber { get; set; }


		[Display(Name = "")]
		public QuestionType QuestionType { get; set; }


		[Display(Name = "")]
		public int QuestionPoint { get; set; }


		[Display(Name = "")]
		public QuestionHardnessType QuestionHardnessType { get; set; }


		[Display(Name = "")]
		public ReapetnessType ReapetnessType { get; set; }


		[Display(Name = "")]
		public bool UseEvaluation { get; set; }


		[Display(Name = "")]
		public bool IsStandard { get; set; }


		[Display(Name = "")]
		public byte AuthorType { get; set; }


		[Display(Name = "")]
		public string AuthorName { get; set; }


		[Display(Name = "")]
		public short ResponseSecond { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


		[Display(Name = "")]
		public DateTime InsertDateTime { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public int AxillaryBookId { get; set; }


	}
}
