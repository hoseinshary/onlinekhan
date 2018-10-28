using System.ComponentModel.DataAnnotations;
using System.Linq;
using NasleGhalam.ViewModels._Attributes;
using NasleGhalam.ViewModels.Lookup;
namespace NasleGhalam.ViewModels.EducationTree
{
    public class EducationGroupGetAllViewModel
    {

        public int Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }


        [Display(Name = "نوع درخت آموزش")]
        public int LookupId_EducationTreeState { get; set; }

        [Display(Name = "درخت آموزش پدر")]
        public int? ParentEducationTreeId { get; set; }

        [Display(Name = "گروه آموزشی")]
        public LookupGetViewModel EducationGroup { get; set; }

    }
}
