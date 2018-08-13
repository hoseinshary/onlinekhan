using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.EducationGroup
{
    public class EducationGroupIsCheckedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
