using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


    }
}
