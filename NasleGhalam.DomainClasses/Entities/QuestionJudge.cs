namespace NasleGhalam.DomainClasses.Entities
{
    public class QuestionJudge
    {
        public QuestionJudge()
        {
        }
        public int Id { get; set; }

        public byte Hardness { get; set; }

        public byte Repeatness { get; set; }

        public bool IsStandard { get; set; }

        public short ResponseSecond { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
