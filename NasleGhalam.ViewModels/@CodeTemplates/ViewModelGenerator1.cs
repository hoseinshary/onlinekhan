using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Exam
{
	public class ExamViewModel
	{
		
		public int Id { get; set; }


		[Display(Name = "نام")]
		public string Name { get; set; }


		[Display(Name = "تاریخ امتحان")]
		public DateTime Date { get; set; }

        [Display(Name = "گروه درسی")]
        public string EducationGroupName { get; set; }   

        public int EducationGroupId { get; set; }

		[Display(Name = "سال تحصیلی")]
        public string EducationYearName { get; set; }
        public int EducationYearId { get; set; }


	}
}
