using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Assay
{
    public class AssayViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public string Title { get; set; }


        [Display(Name = "")]
        public int Time { get; set; }


        [Display(Name = "")]
        public int LookupId_Importance { get; set; }


        [Display(Name = "")]
        public int LookupId_Type { get; set; }


        [Display(Name = "")]
        public int LookupId_QuestionType { get; set; }


        [Display(Name = "")]
        public bool IsPublic { get; set; }


        [Display(Name = "")]
        public bool IsOnline { get; set; }


        [Display(Name = "")]
        public int UserId { get; set; }


        [Display(Name = "")]
        public DateTime DateTimeCreate { get; set; }


  


    }
}
