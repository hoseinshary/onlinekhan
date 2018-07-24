using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
	public class EducationSubGroupViewModel
	{
	
		public int Id { get; set; }


		[Display(Name = "نام")]
		public string Name { get; set; }


		[Display(Name = "گروه تحصیلی")]
		public int EducationGroupId { get; set; }


	}
}
