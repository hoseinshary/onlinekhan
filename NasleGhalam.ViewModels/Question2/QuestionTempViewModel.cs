using NasleGhalam.ViewModels._MediaFormatter;

namespace NasleGhalam.ViewModels.Question2
{
    public class QuestionTempViewModel : IMultiPartMediaTypeFormatter
    {
       
        
        public string Context { get; set; }


        public byte[] wordFile { get; set; }

   

    }
}
