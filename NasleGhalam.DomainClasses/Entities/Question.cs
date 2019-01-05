using System;
using System.Collections.Generic;

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

        public int LookupId_RepeatnessType { get; set; }
        public Lookup Lookup_RepeatnessType { get; set; }

        public bool UseEvaluation { get; set; }

        public bool IsStandard { get; set; }

        public int LookupId_AuthorType { get; set; }

        public Lookup Lookup_AuthorType { get; set; }

        public string AuthorName { get; set; }

        public short ResponseSecond { get; set; }
        public int LookupId_AreaType { get; set; }
        public Lookup Lookup_AreaType { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public DateTime InsertDateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool IsActive { get; set; }

        public int AnswerNumber { get; set; }

        public ICollection<QuestionJudge> QuestionJudges { get; set; }
               
        public ICollection<QuestionOption> QuestionOptions { get; set; }
               
        public ICollection<QuestionEqual> QuestionEquals1 { get; set; }
               
        public ICollection<QuestionEqual> QuestionEquals2 { get; set; }
               
        public ICollection<Tag> Tags { get; set; }

        public ICollection<Box> Boxes { get; set; }
     
        public ICollection<QuestionAnswer> Answers { get; set; }

        public ICollection<Topic> Topics { get; set; }
        public ICollection<QuestionGroup> QuestionGroups { get; set; }
    }
}
