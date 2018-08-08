using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
            QuestionAnswerViews = new HashSet<QuestionAnswerView>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Context { get; set; }

        public string FilePath { get; set; }

        public int LookupId_AnswerType { get; set; }
        public Lookup Lookup_AnswerType { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public bool IsMaster { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public virtual ICollection<QuestionAnswerView> QuestionAnswerViews { get; set; }
    }
}
