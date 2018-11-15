using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using NasleGhalam.ViewModels._MediaFormatter;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.Question
{
    public class 
        QuestionViewModel :IMultiPartMediaTypeFormatter
    {
        
        public int Id { get; set; }


        
        public string Context { get; set; }


        
        public int QuestionNumber { get; set; }


        
        public int LookupId_QuestionType { get; set; }


        
        public int QuestionPoint { get; set; }


        
        public int LookupId_QuestionHardnessType { get; set; }


        
        public int LookupId_RepeatnessType { get; set; }


        
        public bool UseEvaluation { get; set; }


        
        public bool IsStandard { get; set; }



        
        public int LookupId_AuthorType { get; set; }

        
        public string AuthorName { get; set; }


        
        public int LookupId_AreaType { get; set; }


        
        public short ResponseSecond { get; set; }


        public string Description { get; set; }

        public string FileName { get; set; }


      
        public DateTime InsertDateTime { get; set; }


        public int UserId { get; set; }

      
        public bool IsActive { get; set; }


        private List<int> topicsId = new List<int>();

        private List<int> tagsId = new List<int>();

        public List<int> TopicsId { get => topicsId; set => topicsId = value; }
        public List<int> TagsId { get => tagsId; set => tagsId = value; }
    }
}
