using System.ComponentModel.DataAnnotations;
using NasleGhalam.ViewModels._Attributes;

namespace NasleGhalam.ViewModels.User
{
    public class UserGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string NationalNo { get; set; }
        public bool Gender { get; set; }
        public string GenderName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}

