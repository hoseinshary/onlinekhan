using System.Collections.Generic;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.LessonDepartment;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public LessonViewModel()
        {
            NumberOfJudges = 1;
        }
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsMain { get; set; }
        
        public int LookupId_Nezam { get; set; }

        public int NumberOfJudges { get; set; }

        public IEnumerable<EducationTreeViewModel> EducationTrees { get; set; }

        public IEnumerable<RatioViewModel> Ratios { get; set; }

        public IEnumerable<LessonDepartmentViewModel> LessonDepartments { get; set; }
    }
}
