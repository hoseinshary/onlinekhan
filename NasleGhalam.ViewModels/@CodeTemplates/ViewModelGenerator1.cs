﻿using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Topic
{
	public class TopicViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Title { get; set; }


		[Display(Name = "")]
		public int ExamStock { get; set; }


		[Display(Name = "")]
		public int ExamStockSystem { get; set; }


		[Display(Name = "")]
		public short Importance { get; set; }


		[Display(Name = "")]
		public bool IsExamSource { get; set; }


		[Display(Name = "")]
		public TopicHardnessType HardnessType { get; set; }


		[Display(Name = "")]
		public AreaType AreaType { get; set; }


		[Display(Name = "")]
		public bool IsActive { get; set; }


		[Display(Name = "")]
		public int EducationGroup_LessonId { get; set; }


	}
}
