using System;
using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Question
    {
        public Question()
        {
            QuestionJudges = new HashSet<QuestionJudge>();
            QuestionOptions = new HashSet<QuestionOption>();
            QuestionEquals1 = new HashSet<QuestionEqual>();
            QuestionEquals2 = new HashSet<QuestionEqual>();
            Tags = new HashSet<Tag>();
            Boxes = new HashSet<Box>();
            Answers = new HashSet<QuestionAnswer>();
            Topics = new HashSet<Topic>();
        }
        public int Id { get; set; }

        public string Context { get; set; }

        public int QuestionNumber { get; set; }

        public int LookupId_QuestionType { get; set; }
        public Lookup Lookup_QuestionType { get; set; }

        public int QuestionPoint { get; set; }

        public int LookupId_QuestionHardnessType { get; set; }
        public Lookup Lookup_QuestionHardnessType { get; set; }

        public int LookupId_ReapetnessType { get; set; }
        public Lookup Lookup_ReapetnessType { get; set; }

        public bool UseEvaluation { get; set; }

        public bool IsStandard { get; set; }

        public byte AuthorType { get; set; }

        public string AuthorName { get; set; }

        public short ResponseSecond { get; set; }

        public string Description { get; set; }

        public DateTime InsertDateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int AxillaryBookId { get; set; }

        public AxillaryBook AxillaryBook { get; set; }

        public virtual ICollection<QuestionJudge> QuestionJudges { get; set; }
               
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
               
        public virtual ICollection<QuestionEqual> QuestionEquals1 { get; set; }
               
        public virtual ICollection<QuestionEqual> QuestionEquals2 { get; set; }
               
        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Box> Boxes { get; set; }

        public virtual ICollection<QuestionAnswer> Answers { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
