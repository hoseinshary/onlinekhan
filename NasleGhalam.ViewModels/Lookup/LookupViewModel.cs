using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Lookup
{
    public class LookupViewModel
    {
        
        public int Id { get; set; }


        [Display(Name = "لوک آپ")]
        public string Name { get; set; }


        [Display(Name = "")]
        public string Value { get; set; }


        [Display(Name = "")]
        public int State { get; set; }


    }
}
