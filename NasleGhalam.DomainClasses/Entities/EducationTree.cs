using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.DomainClasses.Entities
{
    public class EducationTree
    {
        public EducationTree()
        {
            this.Exams = new HashSet<Exam>();
            this.EducationSubGroups = new HashSet<EducationSubGroup>();
            this.HistoryEducations = new HashSet<HistoryEducation>();
            this.Lessons = new HashSet<Lesson>();
            this.ChildrenEducationTree = new HashSet<EducationTree>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentEducationTreeId { get; set; }

        public int LookupId_EducationTreeState { get; set; }

        public Lookup Lookup_EducationTreeState { get; set; }

        public EducationTree ParentEducationTree { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }
    
        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<EducationSubGroup> EducationSubGroups { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public ICollection<EducationTree> ChildrenEducationTree { get; set; }

    }
}
