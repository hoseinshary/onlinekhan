using System.ComponentModel.DataAnnotations;
using System;
using NasleGhalam.ViewModels._MediaFormatter;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionTempViewModel : IMultiPartMediaTypeFormatter
    {
       
        
        public string Context { get; set; }


        public byte[] wordFile { get; set; }


    }
}
