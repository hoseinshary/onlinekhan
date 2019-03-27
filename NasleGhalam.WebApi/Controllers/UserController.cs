using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.User;
using NasleGhalam.WebApi.Extensions;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.03.28
    /// </author>
    public class UserController : ApiController
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet, CheckUserAccess(ActionBits.UserReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_userService.GetAll(Request.GetRoleLevel()));
        }

        [HttpGet, CheckUserAccess(ActionBits.UserReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var user = _userService.GetById(id, Request.GetRoleLevel());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.UserCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(UserCreateViewModel userViewModel)
        {
            var msgRes = _userService.Create(userViewModel, Request.GetRoleLevel());
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.UserUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(UserUpdateViewModel userViewModel)
        {
            var msgRes = _userService.Update(userViewModel, Request.GetRoleLevel());
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.UserDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _userService.Delete(id, Request.GetRoleLevel());
            return Ok(msgRes);
        }

        [HttpPost]
        public IHttpActionResult Login(LoginViewModel login)
        {
            
            return Ok(_userService.Login(login));
        }
    }
}
