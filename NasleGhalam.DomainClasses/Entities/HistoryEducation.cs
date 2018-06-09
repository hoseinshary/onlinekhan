using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class HistoryEducation
    {
        public HistoryEducation()
        {
            UniversityBranches_HistoryEducations = new HashSet<UniversityBranch_HistoryEducation>();
        }
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public int EducationSubGroupId { get; set; }

        public EducationSubGroup EducationSubGroup { get; set; }

        public int EducationGroupId { get; set; }

        public EducationGroup EducationGroup { get; set; }

        public int GradeLevelId { get; set; }

        public GradeLevel GradeLevel { get; set; }

        public virtual ICollection<UniversityBranch_HistoryEducation> UniversityBranches_HistoryEducations { get; set; }
    }
}
