namespace NasleGhalam.ViewModels.Topic
{
    public class TopicGetViewModel
    {

        public int Id { get; set; }


        
        public string Title { get; set; }


        
        public int ExamStock { get; set; }


        
        public int ExamStockSystem { get; set; }


        
        public short Importance { get; set; }


        
        public bool IsExamSource { get; set; }


        
        public int LookupId_HardnessType { get; set; }


        
        public int LookupId_AreaType { get; set; }


        
        public bool IsActive { get; set; }


        
        public int EducationGroup_LessonId { get; set; }

        public int LessonId { get; set; }
        public int EducationGroupId { get; set; }
        public string LessonName { get; set; }
        public string EducationGroupName { get; set; }


        public int? ParentTopicId { get; set; }

        public string ParentTopicTitle { get; set; }

    }
}
