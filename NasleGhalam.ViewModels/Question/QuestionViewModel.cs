﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using NasleGhalam.ViewModels._MediaFormatter;
using NasleGhalam.ViewModels._Attributes;
using NasleGhalam.ViewModels.QuestionOption;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.ViewModels.Tag;
using System.Linq;

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


        public IEnumerable<TopicViewModel> Topics { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public List<QuestionOptionViewModel> QuestionOptions { get; set; }

        public QuestionViewModel()
        {
            //QuestionOptions = new 

        }

        //public bool isValid => QuestionOptions.Count(x => x.IsAnswer == true) == 1;  
    }
}
