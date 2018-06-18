using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.User
{
    public class LoginResultViewModel
    {
        public MessageType MessageType { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public string DefaultPage { get; set; }
    }
}
