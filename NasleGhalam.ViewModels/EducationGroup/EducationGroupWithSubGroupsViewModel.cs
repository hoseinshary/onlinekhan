
using System.Collections.Generic;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupWithSubGroupsViewModel
    {
        public EducationGroupIsCheckedViewModel EducationGroups { get; set; }
        public List<EducationSubGroupViewModel> SubGroups { get; set; }
    }
}
