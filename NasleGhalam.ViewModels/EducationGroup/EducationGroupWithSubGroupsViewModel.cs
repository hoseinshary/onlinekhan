
using System.Collections.Generic;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupWithSubGroupsViewModel
    {
        public EducationGroupIsCheckedViewModel EducationGroup { get; set; }
        public List<EducationSubGroupLessonViewModel> SubGroups { get; set; }
    }
}
