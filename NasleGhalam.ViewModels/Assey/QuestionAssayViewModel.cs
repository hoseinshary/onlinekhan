using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ViewModels.Assey
{
    public class QuestionAssayViewModel
    {
        public int Id { get; set; }

        public string Context { get; set; }

        public int LessonId { get; set; }

        public string LessonName { get; set; }

        public int TopicId { get; set; }

        public int QuestionPoint { get; set; }

        public bool UseEvaluation { get; set; }

        public bool IsStandard { get; set; }

        public string AuthorName { get; set; }

        public short ResponseSecond { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
        public int AnswerNumber { get; set; }
        public int LookupId_QuestionType { get; set; }

        public int LookupId_QuestionHardnessType { get; set; }

        public int LookupId_RepeatnessType { get; set; }

        public int LookupId_AuthorType { get; set; }

        public int LookupId_AreaType { get; set; }
    }
}
