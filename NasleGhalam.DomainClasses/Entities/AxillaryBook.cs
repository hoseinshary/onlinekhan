using System.Collections.Generic;
using System.Security.AccessControl;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AxillaryBook
    {
        public AxillaryBook()
        {
            EducationBooks = new HashSet<EducationBook>();
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public short PublishYear { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }


        public string Font { get; set; }

        public int PrintTypeId { get; set; }
        public LookUp PrintType { get; set; }

        public string ImgPath { get; set; }

        public int Price { get; set; }

        public int OriginalPrice { get; set; }
        public int BookTypeId { get; set; }
        public LookUp BookType { get; set; }

        public int PaperTypeId { get; set; }
        public LookUp PaperType { get; set; }

        public bool HasImage { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public virtual ICollection<EducationBook> EducationBooks { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
