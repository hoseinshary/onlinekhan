using System.Collections.Generic;


namespace NasleGhalam.DomainClasses.Entities
{
    public class LookUp
    {
        public LookUp()
        {
            AxillaryBooks = new HashSet<AxillaryBook>();
            QuestionAnswers = new HashSet<QuestionAnswer>();
            Questions = new HashSet<Question>();
            Topics = new HashSet<Topic>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int State { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<AxillaryBook> AxillaryBooks { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
