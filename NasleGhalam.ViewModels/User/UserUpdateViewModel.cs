﻿using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.User
{
    public class UserUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen")]
        [Display(Name = "نام")]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }


        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }


        [MaxLength(50, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
       //[MinLength(5, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen")]
        [Display(Name = "رمز عبور")]
        [MinLengthExcludeEmptyAndNull(5, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MinLen2")]
        public string Password { get; set; }


        //[Display(Name = "ادمین")]
        //public bool IsAdmin { get; set; }


        [Display(Name = "فعال")]
        public bool IsActive { get; set; }


        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 11, MinimumLength = 10, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RangeLength")]
        [NationalNo(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "NationalNo")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "کد ملی")]
        public string NationalNo { get; set; }


        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }


        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Length")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "تلفن ثابت")]
        public string Phone { get; set; }


        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Length")]
        [RegularExpression("\\d+", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Numeric")]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }


        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        [Display(Name = "نقش")]
        public int RoleId { get; set; }

        public string RoleName { get; set; }


        [RequiredDdlValidator(invalidValue: "0", ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "RequiredDll")]
        [Display(Name = "شهر")]
        public int CityId { get; set; }

        public string CityName { get; set; }
    }
}

