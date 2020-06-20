using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.ProgramItem
{
	public class ProgramItemViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public int LookupId_PrgoramItemName { get; set; }


		[Display(Name = "")]
		public int ProgramId { get; set; }


		[Display(Name = "")]
		public int Hour { get; set; }


	}
}
