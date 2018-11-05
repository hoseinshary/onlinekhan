using System.Collections.Generic;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsMain { get; set; }
        
        public int LookupId_Nezam { get; set; }

        public IEnumerable<EducationTreeViewModel> EducationTrees { get; set; }

        public IEnumerable<RatioViewModel> Ratios { get; set; }
    }
}
