﻿using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class StudentQuestionAssayReportViewModel
    {
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        public UserViewModel User { get; set; }
    }
}

