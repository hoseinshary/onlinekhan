using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class UniversityBranch
    {
        public UniversityBranch()
        {
            UniversityBranches_HistoryEducations = new HashSet<UniversityBranch_HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public int EducationSubGroupId { get; set; }

        public EducationSubGroup EducationSubGroup { get; set; }

        public virtual ICollection<UniversityBranch_HistoryEducation> UniversityBranches_HistoryEducations { get; set; }
    }
}
