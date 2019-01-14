using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.EducationTree;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
    public class EducationSubGroupViewModel
    {
      
        public int Id { get; set; }


        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "درخت آموزش")]
        public int EducationTreeId { get; set; }

        public EducationTreeViewModel EducationTree { get; set; }


    }
}
