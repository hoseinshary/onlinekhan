using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ViewModels.Lesson
{
	public class LessonViewModel
	{
	    
        public int Id { get; set; }


	    [Display(Name = "نام")]
	    [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
	    [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


		[Display(Name = "اختصاصی")]
		public bool IsMain { get; set; }

	    [Display(Name = "گروه آموزشی")]
	    [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public List<int> EducationGroupId { get; set; }

	    [Display(Name = "ضرایب")]
	    [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]

	    public List<List<Rate>> List1 { get; set; }






    }
}
