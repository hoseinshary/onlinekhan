using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
    public class AssayAnswerSheetViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public int AssayId { get; set; }


        [Display(Name = "")]
        public int UserId { get; set; }


        [Display(Name = "")]
        public AssayVarient AssayVarient { get; set; }


        [Display(Name = "")]
        public DateTime AssayTime { get; set; }


        [Display(Name = "")]
        public DateTime DateTime { get; set; }

        public IList<string> AnswerTimes { get; set; }

        public IList<string> Answers { get; set; }

        public IList<bool> MaybeList { get; set; }

        public IList<bool> AgterfList { get; set; }
        public IList<bool> CantList { get; set; }


    }
}