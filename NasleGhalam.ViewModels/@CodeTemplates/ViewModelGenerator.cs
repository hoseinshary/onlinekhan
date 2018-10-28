using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationTree
{
	public class EducationTreeViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public int LookupId_EducationTreeState { get; set; }


	}
}
