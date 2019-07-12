namespace NasleGhalam.DomainClasses.Entities
{
    public class AssayAnswerSheet
    {
        public int Id { get; set; }

        public int AssaySchaduleId { get; set; }

        public AssaySchedule AssaySchadule { get; set; }

        public int AssayQuestionId { get; set; }

        public AssayQuestion AssayQuestion { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int AnswerTime { get; set; }

        public int Answer { get; set; }




    }
}
