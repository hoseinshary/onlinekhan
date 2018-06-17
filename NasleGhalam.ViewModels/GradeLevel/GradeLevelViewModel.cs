using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.GradeLevel
{
    public class GradeLevelViewModel
    {

        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "اولویت نمایش")]
        public byte Priority { get; set; }


        [Display(Name = "مقطع")]
        public int GradeId { get; set; }


        [Display(Name = "مقطع")]
        public string GradeName{ get; set; }

    }
}
