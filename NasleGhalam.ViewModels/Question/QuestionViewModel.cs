using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionViewModel
    {
        
        public int Id { get; set; }


        [Display(Name = "متن سوال")]
        public string Context { get; set; }


        [Display(Name = "شماره سوال در کتاب اصلی")]
        public int QuestionNumber { get; set; }


        [Display(Name = "نوع سوال")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "نمره سوال")]
        public int QuestionPoint { get; set; }

        [Display(Name = "سختی سوال")]
        public QuestionHardnessType QuestionHardnessType { get; set; }

        [Display(Name = "تکرار")]
        public ReapetnessType ReapetnessType { get; set; }

        [Display(Name = "ارزبابی")]
        public bool UseEvaluation { get; set; }

        [Display(Name = "استاندارد")]
        public bool IsStandard { get; set; }

        [Display(Name = "نوع طراح")]
        public byte AuthorType { get; set; }

        [Display(Name = "طراح")]
        public string AuthorName { get; set; }

        [Display(Name = "زمان پاسخ")]
        public short ResponseSecond { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDateTime { get; set; }

        [Display(Name = "کاربر ثبت کننده")]
        public int UserId { get; set; }

        [Display(Name = "کاربر ثبت کننده")]
        public string UserName { get; set; }

        [Display(Name = "کتاب")]
        public int AxillaryBookId { get; set; }

        [Display(Name = "کتاب")]
        public string AxillaryBookName { get; set; }


    }
}
