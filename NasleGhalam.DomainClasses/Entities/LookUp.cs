using System.Collections.Generic;


namespace NasleGhalam.DomainClasses.Entities
{
    public class Lookup
    {
        public Lookup()
        {
            AxillaryBook_PrintTypes = new HashSet<AxillaryBook>();
            AxillaryBook_BookTypes = new HashSet<AxillaryBook>();
            AxillaryBook_PaperTypes = new HashSet<AxillaryBook>();

            QuestionAnswers = new HashSet<QuestionAnswer>();

            Question_QuestionHardnessTypes = new HashSet<Question>();
            Question_QuestionTypes = new HashSet<Question>();
            Question_ReapetnessTypes = new HashSet<Question>();

            Topic_AreaTypes = new HashSet<Topic>();
            Topic_Hardnesses = new HashSet<Topic>();
            Lesson_Nezams = new HashSet<Lesson>();
            EducationTree_States = new HashSet<EducationTree>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int State { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public ICollection<AxillaryBook> AxillaryBook_PrintTypes { get; set; }

        public ICollection<AxillaryBook> AxillaryBook_BookTypes { get; set; }

        public ICollection<AxillaryBook> AxillaryBook_PaperTypes { get; set; }

        public ICollection<Question> Question_QuestionTypes { get; set; }

        public ICollection<Question> Question_QuestionHardnessTypes { get; set; }

        public ICollection<Question> Question_ReapetnessTypes { get; set; }

        public ICollection<Topic> Topic_Hardnesses { get; set; }

        public ICollection<Topic> Topic_AreaTypes { get; set; }

        public ICollection<Lesson> Lesson_Nezams { get; set; }

        public ICollection<EducationTree> EducationTree_States { get; set; }
    }
}
