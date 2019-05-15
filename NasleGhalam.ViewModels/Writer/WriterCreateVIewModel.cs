using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Writer
{
    public class WriterCreateViewModel
    {
       
        [Display(Name = "نام نویسنده")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        
    }
}
