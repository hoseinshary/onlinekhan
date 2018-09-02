using System.ComponentModel.DataAnnotations;
using System;
using NasleGhalam.ViewModels._MediaFormatter;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionCreateViewModel :IMultiPartMediaTypeFormatter
    {
        
        public int Id { get; set; }


        [Display(Name = "متن")]
        public string Context { get; set; }


        [Display(Name = "شماره سوال ")]
        public int QuestionNumber { get; set; }


        [Display(Name = "نوع سوال ")]
        public int LookupId_QuestionType { get; set; }


        [Display(Name = "نمره")]
        public int QuestionPoint { get; set; }


        [Display(Name = "درجه سختی")]
        public int LookupId_QuestionHardnessType { get; set; }


        [Display(Name = "درجه تکرار")]
        public int LookupId_ReapetnessType { get; set; }


        [Display(Name = "ارزیابی")]
        public bool UseEvaluation { get; set; }


        [Display(Name = "استاندارد")]
        public bool IsStandard { get; set; }


        [Display(Name = "نوع طراح")]
        public byte AuthorType { get; set; }


        [Display(Name = "نام نویسنده")]
        public string AuthorName { get; set; }


        [Display(Name = "زمان پاسخ")]
        public short ResponseSecond { get; set; }


        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام فایل")]
        public string FileName { get; set; }


        [Display(Name = "تاریخ ورود داده")]
        public DateTime InsertDateTime { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }





    }
}
