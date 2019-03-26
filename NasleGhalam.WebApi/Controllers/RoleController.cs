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
    ///     date: 1397.03.28
    /// </author>
    public class RoleController : ApiController
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
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
        public IHttpActionResult Create(RoleCreateViewModel roleViewModel)
        {
            var msgRes = _roleService.Create(roleViewModel, Request.GetRoleLevel());
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.RoleUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(RoleUpdateViewModel roleViewModel)
        {
            var msgRes = _roleService.Update(roleViewModel, Request.GetRoleLevel());
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.RoleDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _roleService.Delete(id, Request.GetRoleLevel());
            return Ok(msgRes);
        }
    }
}
