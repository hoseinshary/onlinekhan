using System.Collections.Generic;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ViewModels.Assay
{
    public class LessonAssayViewModel
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public IEnumerable<TopicAssayViewModel> Topics { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
        
        public int CountOfEasy { get; set; }

        public int CountOfMedium { get; set; }

        public int CountOfHard { get; set; }

        public int CountOfQuestions => CountOfEasy + CountOfMedium + CountOfHard;
    }
}
