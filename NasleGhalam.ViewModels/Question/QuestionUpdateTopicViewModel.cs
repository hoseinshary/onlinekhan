using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using NasleGhalam.ViewModels._MediaFormatter;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionUpdateTopicViewModel :IMultiPartMediaTypeFormatter
    {   
        public int Id { get; set; }

        [Display(Name = "حیطه")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AreaType { get; set; }

        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        public int AnswerNumber { get; set; }

        public List<int> TopicsId { get; set; } = new List<int>();
        public List<int> TagsId { get; set; } = new List<int>();
    }
}
