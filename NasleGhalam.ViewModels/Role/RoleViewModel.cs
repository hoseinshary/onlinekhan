using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.Role
{
    public class RoleViewModel
    {
        private static readonly string[] AllUserTypeName = { "سازمانی", "دانش آموز", "معلم" };

        public int Id { get; set; }

        public string Name { get; set; }

        public byte Level { get; set; }

        public string SumOfActionBit { get; set; }

        public UserType UserType { get; set; }

        public string UserTypeName => AllUserTypeName[(int)UserType];
    }
}
