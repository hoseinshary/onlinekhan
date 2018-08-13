
using System.Collections.Generic;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupWithSubGroupsViewModel
    {
        public List<EducationGroupIsCheckedViewModel> EducationGroups { get; set; }
        public List<List<EducationSubGroupViewModel>> SubGroups { get; set; }
    }
}
