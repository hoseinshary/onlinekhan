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

        public ICollection<EducationSubGroup> EducationSubGroups { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }

        public ICollection<EducationGroup_Lesson> EducationGroups_Lessons { get; set; }
    }
}
