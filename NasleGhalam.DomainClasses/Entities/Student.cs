using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Student
    {
        public Student()
        {
            HistoryEducations = new HashSet<HistoryEducation>();
            QuestionAnswerViews = new HashSet<QuestionAnswerView>();
        }
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        public User User { get; set; }

        public virtual ICollection<HistoryEducation> HistoryEducations { get; set; }

        public virtual ICollection<QuestionAnswerView> QuestionAnswerViews { get; set; }
    }
}
