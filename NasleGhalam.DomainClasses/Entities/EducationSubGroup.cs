using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationSubGroup
    {
        public EducationSubGroup()
        {
            UniversityBranches = new HashSet<UniversityBranch>();
            Ratios = new HashSet<Ratio>();
            HistoryEducations = new HashSet<HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int EducationGroupId { get; set; }

        public EducationGroup EducationGroup { get; set; }

        public virtual ICollection<UniversityBranch> UniversityBranches { get; set; }

        public virtual ICollection<Ratio> Ratios { get; set; }

        public virtual ICollection<HistoryEducation> HistoryEducations { get; set; }

    }
}
