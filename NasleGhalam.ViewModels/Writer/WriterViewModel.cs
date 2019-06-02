using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Writer
{
    public class WriterViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? UserId { get; set; }

        public UserViewModel User { get; set; }
    }
}
