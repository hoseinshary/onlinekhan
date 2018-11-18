using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationBook
{
	public class EducationBookViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public short PublishYear { get; set; }


		[Display(Name = "")]
		public bool IsExamSource { get; set; }


		[Display(Name = "")]
		public bool IsActive { get; set; }


		[Display(Name = "")]
		public bool IsChanged { get; set; }


		[Display(Name = "")]
		public int LessonId { get; set; }


	}
}
