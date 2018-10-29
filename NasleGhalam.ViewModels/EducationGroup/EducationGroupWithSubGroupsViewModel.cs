
using System.Collections.Generic;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupWithSubGroupsViewModel
    {
        public int EducationGroupId { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

     //   public List<EducationSubGroupLessonViewModel> SubGroups { get; set; }
    }
}
