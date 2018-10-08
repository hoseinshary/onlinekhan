using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class City
    {
        public City()
        {
            Users = new HashSet<User>();
            HistoryEducations = new HashSet<HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProvinceId { get; set; }

        public  Province Province { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<HistoryEducation> HistoryEducations { get; set; }

    }
}
