using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Lesson
    {
        public Lesson()
        {
            Ratios = new HashSet<Ratio>();
            EducationGroups_Lessons = new HashSet<EducationGroup_Lesson>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        public virtual ICollection<Ratio> Ratios { get; set; }

        public virtual ICollection<EducationGroup_Lesson> EducationGroups_Lessons { get; set; }
    }
}
