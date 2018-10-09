using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Grade
    {
        public Grade()
        {
            GradeLevels = new HashSet<GradeLevel>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Priority { get; set; }

        public ICollection<GradeLevel> GradeLevels { get; set; }
    }
}
