using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionUpdateImportViewModel
    {
        public int Id { get; set; }      

        [Display(Name = "شماره سوال ")]
        public int QuestionNumber { get; set; }

        [Display(Name = "نوع سوال ")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionType { get; set; }

        [Display(Name = "نوع طراح")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AuthorType { get; set; }


        [Display(Name = "نویسنده")]
        public int WriterId { get; set; }




        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        public int AnswerNumber { get; set; }

        public List<int> TagIds { get; set; } = new List<int>();
    }
}
