using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Log
{
	public class LogViewModel
	{
		[Display(Name = "")]
		public Guid Id { get; set; }


		[Display(Name = "")]
		public string TableName { get; set; }


		[Display(Name = "")]
		public CrudType CrudType { get; set; }


		[Display(Name = "")]
		public DateTime DateTime { get; set; }


		[Display(Name = "")]
		public string ObjectId { get; set; }


		[Display(Name = "")]
		public string ObjectValue { get; set; }


		[Display(Name = "")]
		public string BrowserInfo { get; set; }


	}
}
