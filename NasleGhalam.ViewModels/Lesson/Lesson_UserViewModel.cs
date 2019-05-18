using NasleGhalam.ViewModels._Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ViewModels.Lesson
{
    public class Lesson_UserViewModel
    {
        public ICollection<int> UsersId { get; set; }

        public ICollection<int> LessonsId { get; set; }

    }
}
