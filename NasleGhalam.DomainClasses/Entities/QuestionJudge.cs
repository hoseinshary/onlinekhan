namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionJudge
    {
        public QuestionJudge()
        {
        }
        public int Id { get; set; }

        public int LookupId_QuestionHardnessType { get; set; }
        public Lookup Lookup_QuestionHardnessType { get; set; }

        public int LookupId_RepeatnessType { get; set; }
        public Lookup Lookup_RepeatnessType { get; set; }

        public byte Repeatness { get; set; }

        public bool IsStandard { get; set; }

        public bool IsDelete { get; set; }

        public bool IsUpdate { get; set; }

        public bool IsLearning { get; set; }

        public short ResponseSecond { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
