using System;
using System.Collections.Generic;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.QuestionOption;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionViewModel 
    {
        public int Id { get; set; }

        public string Context { get; set; }

        public int QuestionNumber { get; set; }

        public int QuestionPoint { get; set; }

        public bool UseEvaluation { get; set; }

        public bool IsStandard { get; set; }

        public string AuthorName { get; set; }

        public short ResponseSecond { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public DateTime InsertDateTime { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public int AnswerNumber { get; set; }

        public string QuestionWordPath => $"/Api/Question/GetWordFile/{FileName}".ToFullRelativePath();

        public string QuestionPicturePath => $"/Api/Question/GetPictureFile/{FileName}".ToFullRelativePath();

        public int LookupId_QuestionType { get; set; }

        public int LookupId_QuestionHardnessType { get; set; }

        public int LookupId_RepeatnessType { get; set; }

        public int LookupId_AuthorType { get; set; }

        public int LookupId_AreaType { get; set; }

        public List<QuestionOptionViewModel> QuestionOptions { get; set; } = new List<QuestionOptionViewModel>();

        public List<TopicViewModel> Topics { get; set; } = new List<TopicViewModel>();

        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
