using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Assay
    {

        public Assay()
        {
            AssayQuestions = new HashSet<AssayQuestion>();
            AssaySchedules = new HashSet<AssaySchedule>();
            Packages = new HashSet<Package>();
        }
        public int Id { get; set; }

        public string Title { get; set; }
        public int Time { get; set; }
        public int LookupId_Importance { get; set; }

        public Lookup Lookup_Importance { get; set; }

        public int LookupId_Type { get; set; }

        public Lookup Lookup_Type { get; set; }

        public int LookupId_QuestionType { get; set; }

        public Lookup Lookup_QuestionType { get; set; }

        public bool IsPublic { get; set; }
        public bool IsOnline { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public string File { get; set; }


        public ICollection<AssayQuestion> AssayQuestions { get; set; }

        public ICollection<AssaySchedule> AssaySchedules { get; set; }

        public ICollection<Package> Packages { get; set; }



    }
}
