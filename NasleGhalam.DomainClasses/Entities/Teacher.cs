using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Teacher
    {
        public Teacher()
        {
            Boxes = new HashSet<Box>();
        }
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<Box> Boxes { get; set; }

    }
}
