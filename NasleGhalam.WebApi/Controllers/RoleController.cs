using System;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Role;
using NasleGhalam.WebApi.Extentions;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.03.28
    /// </author>
    public class RoleController : ApiController
    {
        private readonly RoleService _roleService;
        private readonly Lazy<ActionService> _actionService;
        public RoleController(RoleService roleService,
            Lazy<ActionService> actionService)
        {
            _roleService = roleService;
            _actionService = actionService;
        }


        [HttpGet, CheckUserAccess(ActionBits.RoleReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_roleService.GetAll(Request.GetRoleLevel()));
        }


        [HttpGet, CheckUserAccess(ActionBits.RoleReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var role = _roleService.GetById(id, Request.GetRoleLevel());
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.RoleCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(RoleViewModel roleViewModel)
        {
            var msgRes = _roleService.Create(roleViewModel, Request.GetRoleLevel());
            return Ok(new MessageResultClient
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.RoleUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(RoleViewModel roleViewModel)
        {
            var msgRes = _roleService.Update(roleViewModel, Request.GetRoleLevel());
            return Ok(new MessageResultClient
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.RoleDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _roleService.Delete(id, Request.GetRoleLevel());
            return Ok(new MessageResultClient
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        #region ### Ddl ###
        [HttpGet, CheckUserAccess(ActionBits.UserCreateAccess, ActionBits.UserUpdateAccess)]
        public IHttpActionResult GetAllDdl()
        {
            return Ok(_roleService.GetAllDdl(Request.GetRoleLevel()));
        }
        #endregion


        #region ### Access ###
        [HttpGet, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult GetAllModuleDdl()
        {
            return Ok(_actionService.Value.GetAllModuleDdl(Request.GetAccess()));
        }


        [HttpGet, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult GetAllControllerByModuleIdDdl(int id)
        {
            return Ok(_actionService.Value.GetAllControllerByModuleIdDdl(id, Request.GetAccess()));
        }


        [HttpGet, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult GetActionByControllerIdAndModuleId(int controllerId, int roleId, int moduleId)
        {
            return Ok(_actionService.Value.GetActionByControllerIdAndModuleId(controllerId, roleId, moduleId, Request.GetRoleLevel()));
        }


        [HttpPost, CheckUserAccess(ActionBits.RoleChangeAccess)]
        public IHttpActionResult ChangeAccess(RoleAccessViewModel roleAccess)
        {
            var msgRes = _roleService.ChangeAccess(roleAccess, Request.GetAccess(), Request.GetRoleLevel());
            return Ok(new MessageResultClient()
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
        #endregion

    }
}
