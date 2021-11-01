using System;
using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AssayAnswerSheet
    {
        public int Id { get; set; }

      

        public int AssayId { get; set; }

        public Assay Assay { get; set; }



        public int UserId { get; set; }

        public User User { get; set; }

        public AssayVarient AssayVarient { get; set; }

        public DateTime AssayTime { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<int> AnswerTimes { get; set; }

        public ICollection<int> Answers { get; set; }




    }
}
