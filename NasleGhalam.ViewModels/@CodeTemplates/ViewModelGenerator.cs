using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Lesson
{
	public class LessonViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public bool IsMain { get; set; }


		[Display(Name = "")]
		public int LookupId_Nezam { get; set; }


	}
}
