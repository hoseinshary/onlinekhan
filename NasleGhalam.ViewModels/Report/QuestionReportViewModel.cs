using System;
using System.Collections.Generic;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.ViewModels.QuestionOption;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.ViewModels.User;
using NasleGhalam.ViewModels.Writer;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionReportViewModel
    {
        public int Id { get; set; }


      
        public bool IsDelete { get; set; }

      

        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public bool IsUpdate { get; set; }



     

        public int LookupId_AuthorType { get; set; }

        public string AuthorTypeName { get; set; }

       
        public int WriterId { get; set; }


        public string WriterName { get; set; }


        public int SupervisorUserId { get; set; }

        public string SupervisorName { get; set; }
     

    }
}
