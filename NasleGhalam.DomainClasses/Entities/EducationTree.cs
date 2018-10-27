﻿using System;
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
            Exams = new HashSet<Exam>();
        
            EducationSubGroups = new HashSet<EducationSubGroup>();
            HistoryEducations = new HashSet<HistoryEducation>();
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentEducationTreeId { get; set; }

        public int LookupId_EducationTree { get; set; }

        public Lookup Lookup_EducationTree { get; set; }

        public EducationTree ParentEducationTree { get; set; }

        public ICollection<HistoryEducation> HistoryEducations { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<EducationSubGroup> EducationSubGroups { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public ICollection<EducationTree> ChildrenEducationTree { get; set; }
    }
}
