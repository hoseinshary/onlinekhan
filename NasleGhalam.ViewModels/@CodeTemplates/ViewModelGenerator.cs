using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.AxillaryBook
{
	public class AxillaryBookViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public short PublishYear { get; set; }


		[Display(Name = "")]
		public string Author { get; set; }


		[Display(Name = "")]
		public string Isbn { get; set; }


		[Display(Name = "")]
		public string Font { get; set; }


		[Display(Name = "")]
		public int LookupId_PrintType { get; set; }


		[Display(Name = "")]
		public string ImgPath { get; set; }


		[Display(Name = "")]
		public int Price { get; set; }


		[Display(Name = "")]
		public int OriginalPrice { get; set; }


		[Display(Name = "")]
		public int LookupId_BookType { get; set; }


		[Display(Name = "")]
		public int LookupId_PaperType { get; set; }


		[Display(Name = "")]
		public bool HasImage { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


		[Display(Name = "")]
		public int PublisherId { get; set; }


	}
}
