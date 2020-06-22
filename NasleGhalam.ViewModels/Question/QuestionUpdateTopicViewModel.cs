using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionUpdateTopicViewModel
    {   
        public int Id { get; set; }

        [Display(Name = "حیطه")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AreaType { get; set; }

        [Display(Name = "سوال ترکیبی")]
        public bool IsHybrid { get; set; }

        public int AnswerNumber { get; set; }


        [Display(Name = "مبحث پاسخ صحیح")]
        public string TopicAnswer { get; set; }

        public List<int> TopicIds { get; set; } = new List<int>();

    }
}
