using System.Collections.Generic;
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

        public PrintType PrintType { get; set; }

        public string ImgPath { get; set; }

        public int Price { get; set; }

        public int OriginalPrice { get; set; }

        //public BookType BookType { get; set; }

        public PaperType PaperType { get; set; }

        public bool HasImage { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public virtual ICollection<EducationBook> EducationBooks { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
