using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Student
{
    public class StudentUpdateViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }


        [Display(Name = "آدرس")]
        public string Address { get; set; }

        public UserUpdateViewModel User { get; set; }
    }
}
