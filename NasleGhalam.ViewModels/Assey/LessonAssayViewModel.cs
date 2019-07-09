﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ViewModels.Assey
{
    public class LessonAssayViewModel
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public IEnumerable<TopicAssayViewModel> Topics { get; set; }

        public int QuestionNum { get; set; }

        public int QuestionNumEasy { get; set; }

        public int QuestionNumMedium { get; set; }

        public int QuestionNumHard { get; set; }

    }
}
