using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.Resume
{
    public class PublicationViewModel
    {
        [Display(Name = "ناشر")]
        public string Publisher { get; set; }

        [Display(Name = "مقطع")]
        public string Grade { get; set; }

        [Display(Name = "درس")]
        public string LessonName { get; set; }

        [Display(Name = "نوع کتاب")]
        public KindRequest KindRequest { get; set; }

        [Display(Name = "سال")]
        public string Year { get; set; }

        [Display(Name = "تالیف یا ویرایش")]
        public bool PublishedOrEdit { get; set; }
    }
}
