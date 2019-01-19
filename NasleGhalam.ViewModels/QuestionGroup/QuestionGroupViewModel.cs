using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.QuestionGroup
{
    public class QuestionGroupViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime InsertTime { get; set; }

        public string PInsertTime => InsertTime.ToPersianDateTime();

        public string WordFile { get; set; }

        public string ExcelFile { get; set; }

        public int LessonId { get; set; }

        public int UserId { get; set; }
    }
}
