namespace NasleGhalam.DomainClasses.Entities
{
    public class UniversityBranch_HistoryEducation
    {
        public UniversityBranch_HistoryEducation()
        {
        }
        public int Id { get; set; }

        public int UniversityBranchId { get; set; }

        public UniversityBranch UniversityBranch { get; set; }

        public int HistoryEducationId { get; set; }

        public HistoryEducation HistoryEducation { get; set; }
    }
}
