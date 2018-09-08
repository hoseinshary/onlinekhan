using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.UniversityBranch
{
	public class UniversityBranchViewModel
	{
		public int Id { get; set; }


		[Display(Name = "نام")]
		[Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
		[MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Name { get; set; }


		[Display(Name = "بالانس")]
		public int Balance { get; set; }


	    [Display(Name = "زیرگروه درسی")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationSubGroupId { get; set; }

	    public string EducationSubGroupName { get; set; }


	}
}
