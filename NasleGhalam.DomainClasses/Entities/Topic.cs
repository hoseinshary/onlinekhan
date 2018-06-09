using System;
using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Topic
    {
        public Topic()
        {
            ParentTopics = new HashSet<Topic>();
            EducationBooks = new HashSet<EducationBook>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public int ExamStock { get; set; }

        public int ExamStockSystem { get; set; }

        public short Importance { get; set; }

        public bool IsExamSource { get; set; }

        public TopicHardnessType HardnessType { get; set; }

        public AreaType AreaType { get; set; }

        public bool IsActive { get; set; }

        public int? ParentTopicId { get; set; }

        public Topic ParentTopic { get; set; }

        public virtual ICollection<Topic> ParentTopics { get; set; }

        public int EducationGroup_LessonId { get; set; }

        public EducationGroup_Lesson EducationGroup_Lesson { get; set; }

        public virtual ICollection<EducationBook> EducationBooks { get; set; }

    }
}
