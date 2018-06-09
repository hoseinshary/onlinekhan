using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Tag
    {
        public Tag()
        {
            Questions=new HashSet<Question>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
