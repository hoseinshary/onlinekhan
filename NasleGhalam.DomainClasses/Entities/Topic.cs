using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Topic
    {
        
        public Topic()
        {
            ParentTopics = new HashSet<Topic>();
            EducationBooks = new HashSet<EducationBook>();
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public int ExamStock { get; set; }

        public int ExamStockSystem { get; set; }

        public short Importance { get; set; }

        public bool IsExamSource { get; set; }

        public int LookupId_HardnessType { get; set; }
        public Lookup Lookup_HardnessType { get; set; }

        public int LookupId_AreaType { get; set; }
        public Lookup Lookup_AreaType { get; set; }

        public bool IsActive { get; set; }

        public int? ParentTopicId { get; set; }

        public Topic ParentTopic { get; set; }

        public virtual ICollection<Topic> ParentTopics { get; set; }

        public int EducationGroup_LessonId { get; set; }

        public EducationGroup_Lesson EducationGroup_Lesson { get; set; }

        public virtual ICollection<EducationBook> EducationBooks { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}
