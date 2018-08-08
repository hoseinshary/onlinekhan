using NasleGhalam.Common;
using NasleGhalam.ViewModels._Attributes;
using System.ComponentModel.DataAnnotations;


namespace NasleGhalam.ViewModels.Topic
{
    public class TopicCreateViewModel
    {
        
        public int Id { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Title { get; set; }


        [Display(Name = "سهمیه در کنکور")]
        public int ExamStock { get; set; }


        [Display(Name = "سهمیه در کنکور سیستمی")]
        public int ExamStockSystem { get; set; }


        [Display(Name = "ضریب اهمیت")]
        public short Importance { get; set; }


        [Display(Name = "مبحث کنکوری")]
        public bool IsExamSource { get; set; }


        [Display(Name = "درجه سختی")]
        //TODO:Complate Lookup Instance
        public int HardnessTypeId { get; set; }


        [Display(Name = "حیطه مبحث")]
        //TODO:Complate Lookup Instance
        public int AreaTypeId { get; set; }


        [Display(Name = "فعال")]
        public bool IsActive { get; set; }


        [Display(Name = "گروه تحصیلی_درس")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int EducationGroup_LessonId { get; set; }

        [Display(Name = "مبحث پدر")]
        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(NasleGhalam.ViewModels.ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        public int? ParentTopicId { get; set; }


    }
}
