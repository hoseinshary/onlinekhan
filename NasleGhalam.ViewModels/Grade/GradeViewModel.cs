using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Grade
{
    public class GradeViewModel
    {
  
        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        public byte Priority { get; set; }


    }
}
