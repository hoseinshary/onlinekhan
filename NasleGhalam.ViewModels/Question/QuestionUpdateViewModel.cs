using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionUpdateViewModel
    {
        public int Id { get; set; }


        [Display(Name = "متن")]
        public string Context { get; set; }


        [Display(Name = "شماره سوال ")]
        public int QuestionNumber { get; set; }

        [Display(Name = "نوع سوال ")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionType { get; set; }

        [Display(Name = "نمره")]
        public int QuestionPoint { get; set; }

        [Display(Name = "درجه سختی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_QuestionHardnessType { get; set; }

        [Display(Name = "درجه تکرار")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_RepeatnessType { get; set; }

        [Display(Name = "ارزیابی")]
        public bool UseEvaluation { get; set; }

        [Display(Name = "استاندارد")]
        public bool IsStandard { get; set; }

        [Display(Name = "نوع طراح")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AuthorType { get; set; }

        [Display(Name = "نام طراح")]
        public string AuthorName { get; set; }

        [Display(Name = "نوع طراح")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int LookupId_AreaType { get; set; }

        [Display(Name = "زمان پاسخ")]
        public short ResponseSecond { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        //hatman por shavad 
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }

        [Display(Name = "تاریخ ورود داده")]
        public DateTime InsertDateTime { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public int AnswerNumber { get; set; }

        public List<int> TopicsId { get; set; } = new List<int>();

        public List<int> TagsId { get; set; } = new List<int>();
    }
}
