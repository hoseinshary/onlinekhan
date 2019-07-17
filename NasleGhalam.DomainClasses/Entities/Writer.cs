using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Writer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public int? User_Id { get; set; }

        public User User { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
