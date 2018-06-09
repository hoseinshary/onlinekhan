using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationGroup
    {
        public EducationGroup()
        {
            EducationSubGroups = new HashSet<EducationSubGroup>();
            Exams = new List<Exam>();
            HistoryEducations = new HashSet<HistoryEducation>();
            EducationGroups_Lessons = new HashSet<EducationGroup_Lesson>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<EducationSubGroup> EducationSubGroups { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual ICollection<HistoryEducation> HistoryEducations { get; set; }

        public virtual ICollection<EducationGroup_Lesson> EducationGroups_Lessons { get; set; }
    }
}
