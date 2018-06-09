using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class GradeLevel
    {
        public GradeLevel()
        {
            HistoryEducations = new HashSet<HistoryEducation>();
            EducationBooks = new HashSet<EducationBook>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Priority { get; set; }

        public int  GradeId { get; set; }

        public Grade Grade { get; set; }

        public virtual ICollection<HistoryEducation> HistoryEducations { get; set; }

        public virtual ICollection<EducationBook> EducationBooks { get; set; }
    }
}
