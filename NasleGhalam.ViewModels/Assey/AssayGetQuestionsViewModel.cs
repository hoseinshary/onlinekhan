using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ViewModels.Assey
{
    public class AssayGetQuestionsViewModel
    {
        public IEnumerable<LessonViewModel> Lessons { get; set; }




    }
}
