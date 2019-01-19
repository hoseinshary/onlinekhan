using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ViewModels.QuestionGroup
{
    public class QuestionGroupCreateViewModel
    {
        public QuestionGroupCreateViewModel()
        {
            InsertTime = DateTime.Now;
        }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        public DateTime InsertTime { get; set; }

        public string WordFile { get; set; }

        public string ExcelFile { get; set; }

        [Display(Name = "نام درس")]
        public int LessonId { get; set; }

        public int UserId { get; set; }

        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }
}
