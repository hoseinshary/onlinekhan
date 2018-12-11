using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class GradeLevel
    {
        public GradeLevel()
        {
            HistoryEducations = new HashSet<HistoryEducation>();
            EducationBooks = new HashSet<EducationBook>();
            Lessons = new HashSet<Lesson>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Priority { get; set; }

        public int  GradeId { get; set; }

        public Grade Grade { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }

        public ICollection<EducationBook> EducationBooks { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
