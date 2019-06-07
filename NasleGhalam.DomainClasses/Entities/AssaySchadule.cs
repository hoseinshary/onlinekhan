using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AssaySchadule
    {
        public AssaySchadule()
        {
            AssayAnswerSheets = new HashSet<AssayAnswerSheet>();
        }
        public int Id { get; set; }

        public int AssayId { get; set; }

        public Assay Assay { get; set; }

        public DateTime DateTime { get; set; }

        public int Time { get; set; }

        public ICollection<AssayAnswerSheet> AssayAnswerSheets { get; set; }



    }
}
