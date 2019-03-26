using System;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Role;
using NasleGhalam.WebApi.Extensions;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1398.01.06
    /// </author>
    public class AccessController : ApiController
    {
        private readonly Lazy<ActionService> _actionService;
        private readonly Lazy<RoleService> _roleService;
        public AccessController(Lazy<RoleService> roleService,
            Lazy<ActionService> actionService)
        {
            _roleService = roleService;
            _actionService = actionService;
        }

        [HttpGet, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult GetMenu()
        {
            return Ok(_actionService.Value.GetMenu(Request.GetAccess()));
        }

        [HttpGet, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult GetSubMenu()
        {
            return Ok(_actionService.Value.GetSubMenu(Request.GetAccess()));
        }

        [HttpGet, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult GetAllActions(int roleId)
        {
            return Ok(_actionService.Value.GetAllActions(roleId, Request.GetRoleLevel()));
        }

        [HttpPost, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult ChangeAccess(RoleAccessViewModel roleAccess)
        {
            var msgRes = _roleService.Value.ChangeAccess(roleAccess, Request.GetAccess(), Request.GetRoleLevel());
            return Ok(msgRes);
        }
    }
}
