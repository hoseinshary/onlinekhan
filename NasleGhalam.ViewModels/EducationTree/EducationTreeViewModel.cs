using System.ComponentModel.DataAnnotations;
using System.Linq;
using NasleGhalam.ViewModels._Attributes;
using  NasleGhalam.ViewModels.Lookup;
namespace NasleGhalam.ViewModels.EducationTree
{
    public class EducationTreeViewModel
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }


        
        public int LookupId_EducationTreeState { get; set; }

        
        public int? ParentEducationTreeId { get; set; }

        public LookupViewModel Lookup_EducationTreeState { get; set; }

    }
}
