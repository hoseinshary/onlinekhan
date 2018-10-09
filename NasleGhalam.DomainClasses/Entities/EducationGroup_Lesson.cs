using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationGroup_Lesson
    {
        public EducationGroup_Lesson()
        {
            EducationBooks = new HashSet<EducationBook>();
            Topics = new HashSet<Topic>();
        }
        public int Id { get; set; }

        public int EducationGroupId { get; set; }

        public EducationGroup EducationGroup { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public ICollection<EducationBook> EducationBooks { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
