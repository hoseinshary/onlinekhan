using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Media
{
	public class MediaViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Title { get; set; }


		[Display(Name = "")]
		public int LookupId_MediaType { get; set; }


		[Display(Name = "")]
		public int WriterId { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


		[Display(Name = "")]
		public string FileName { get; set; }


		[Display(Name = "")]
		public int Price { get; set; }


		[Display(Name = "")]
		public DateTime InsertDateTime { get; set; }


		[Display(Name = "")]
		public int UserId { get; set; }


		[Display(Name = "")]
		public bool IsActive { get; set; }


	}
}
