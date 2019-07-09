using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class AssayQuestion
    {
        public AssayQuestion()
        {
            AssayAnswerSheets = new HashSet<AssayAnswerSheet>();
        }
        public int Id { get; set; }

        public int AssayId { get; set; }

        public Assay Assay { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public string File { get; set; }

        public int AnswerNumber { get; set; }

        public ICollection<AssayAnswerSheet> AssayAnswerSheets { get; set; }
        

    }
}
