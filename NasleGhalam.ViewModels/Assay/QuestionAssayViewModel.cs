using System.Collections.Generic;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ViewModels.Assay
{
    public class QuestionAssayViewModel
    {
        public int LessonId { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
