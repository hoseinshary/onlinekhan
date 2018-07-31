using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Action;
using NasleGhalam.ViewModels.Controller;
using Action = NasleGhalam.DomainClasses.Entities.Action;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ActionService
    {
        private readonly IDbSet<Action> _actions;
        private readonly Lazy<RoleService> _roleService;

        public ActionService(IUnitOfWork uow, Lazy<RoleService> roleService)
        {
            _actions = uow.Set<Action>();
            _roleService = roleService;
        }

        /// <summary>
        /// گرفتن  اکشن بیت
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<Action> GetActionByIds(int[] ids)
        {
            return _actions
                .Where(current => ids.Contains(current.Id))
                .ToList();
        }


        /// <summary>
        /// گرفتن اکشن های یک کنترلر
        /// </summary>
        /// <param name="controllerId"></param>
        /// <param name="roleId"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<ActionViewModel> GetActionByControllerId(int controllerId, int roleId, byte userRoleLevel)
        {
            var role = _roleService.Value.GetById(roleId, userRoleLevel);
            if (role == null)
                return new List<ActionViewModel>();

            return _actions
                .Include(current => current.Controller)
                .Where(current => controllerId == 0 || current.ControllerId == controllerId)
                .OrderBy(current => current.Controller.Priority)
                .ThenBy(current => current.Priority)
                .AsEnumerable()
                .Select(current => new ActionViewModel
                {
                    Id = current.Id,
                    ActionFaName = current.FaName,
                    ControllerFaName = current.Controller.FaName,
                    IsChecked = Utility.HasAccess(role.SumOfActionBit, current.ActionBit)
                }).ToList();
        }


        /// <summary>
        /// گرفتن منو
        /// </summary>
        /// <param name="userAccess"></param>
        /// <returns></returns>
        public IList<ControllerViewModel> GetMenu(string userAccess)
        {
            return _actions
                .Include(current => current.Controller)
                .Include(current => current.Controller.Actions)
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Priority)
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => new ControllerViewModel()
                {
                    FaName = current.Controller.FaName,
                    EnName = current.Controller.EnName,
                    Icon = current.Controller.Icone,
                    UserAccess = current.Controller.Actions
                        .Where(x => !x.IsIndex)
                        .Where(x => Utility.HasAccess(userAccess, x.ActionBit))
                        .Select(x => x.FaName).ToArray()
                }).ToList();
        }


        /// <summary>
        /// گرفتن منو برای لیست کشویی با اعمال دسترسی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllControllerByModuleIdDdl(int moduleId, string userAccess)
        {
            return _actions
                .Where(current => current.IsIndex)
                .Where(current => current.Controller.ModuleId == moduleId)
                .OrderBy(current => current.Controller.Priority)
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => new SelectViewModel
                {
                    value = current.ControllerId,
                    label = current.Controller.FaName
                }).ToList();
        }


        /// <summary>
        /// گرفتن  ماژول برای لیست کشویی با اعمال دسترسی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllModuleDdl(string userAccess)
        {
            return _actions
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Module.Priority)
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => new SelectViewModel
                {
                    value = current.Controller.ModuleId,
                    label = current.Controller.Module.Name
                }).ToList();
        }
    }
}
