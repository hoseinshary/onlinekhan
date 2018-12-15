using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.QuestionOption;

namespace NasleGhalam.ViewModels.QuestionGroup
{
    public class QuestionGroupCreateViewModel
    {



        [Display(Name = "")]
        public string Title { get; set; }


        [Display(Name = "")]
        public DateTime InsertTime { get; set; }


        [Display(Name = "")]
        public string WordFile { get; set; }


        [Display(Name = "")]
        public string ExcelFile { get; set; }


        [Display(Name = "")]
        public int LessonId { get; set; }


        [Display(Name = "")]
        public int UserId { get; set; }

        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();



    }
}
