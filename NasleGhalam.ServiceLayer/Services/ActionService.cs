using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Action;
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
        /// گرفتن منو برای لیست کشویی با اعمال دسترسی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllControllerByModuleIdDdl(int moduleId, string userAccess)
        {
            var controller = _actions
                .Include(current => current.Controller)
                .Where(current => current.IsIndex)
                .Where(current => moduleId == 0 || current.Controller.ModuleId == moduleId)
                .OrderBy(current => current.Controller.Priority)
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => new SelectViewModel
                {
                    value = current.ControllerId,
                    label = current.Controller.FaName
                }).ToList();

            controller.Insert(0, new SelectViewModel()
            {
                label = "همه",
                value = 0
            });

            return controller;
        }


        /// <summary>
        /// گرفتن  منو با اعمال دسترسی
        /// </summary>
        /// <returns></returns>
        public IList<MenuViewModel> GetMenu(string userAccess)
        {
            return GetAllModuleQuery(userAccess).ToList();
        }


        /// <summary>
        /// گرفتن زیر منو
        /// </summary>
        /// <param name="userAccess"></param>
        /// <returns></returns>
        public IList<SubMenuViewModel> GetSubMenu(string userAccess)
        {
            return _actions
                .Include(current => current.Controller.Actions)
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Priority)
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => new SubMenuViewModel()
                {
                    ModuleId = current.Controller.ModuleId,
                    FaName = current.Controller.FaName,
                    EnName = current.Controller.EnName,
                    Icon = current.Controller.Icone,
                    UserAccess = current.Controller.Actions
                        .Where(x => !x.IsIndex)
                        .Where(x => Utility.HasAccess(userAccess, x.ActionBit))
                        .Select(x => x.FaName).ToArray()
                }).ToList();

            //return _modules
            //    .Include(current => current.Controllers)
            //    .OrderBy(current => current.Priority)
            //    .Select(current => new
            //    {
            //        current.Name,
            //        controllers = current.Controllers
            //            .Where(ctrl => ctrl.Actions.Any(act => act.IsIndex))
            //            .OrderBy(ctrl => ctrl.Priority)
            //            .Select(ctrl => new
            //            {
            //                ctrl.FaName,
            //                ctrl.EnName,
            //                Icon = ctrl.Icone,
            //                Actions = ctrl.Actions
            //                    .Where(act => !act.IsIndex)
            //                    .Select(x => new
            //                    {
            //                        x.ActionBit,
            //                        x.FaName
            //                    })
            //            })
            //    })
            //    .AsEnumerable()
            //    .Select(current => new MenuViewModel
            //    {
            //        Name = current.Name,
            //        SubMenus = current.controllers
            //            .Where(ctrl => ctrl.Actions.Any(act => Utility.HasAccess(userAccess, act.ActionBit)))
            //            .Select(ctrl => new SubMenuViewModel
            //            {
            //                FaName = ctrl.FaName,
            //                EnName = ctrl.EnName,
            //                Icon = ctrl.Icon,
            //                UserAccess = ctrl.Actions
            //                    .Where(x => Utility.HasAccess(userAccess, x.ActionBit))
            //                    .Select(x => x.FaName).ToArray()
            //            }).ToArray()
            //    })
            //    .ToList();
        }


        /// <summary>
        /// گرفتن  منو برای لیست کشویی با اعمال دسترسی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllModuleDdl(string userAccess)
        {
            var module = GetAllModuleQuery(userAccess)
                .Select(current => new SelectViewModel
                {
                    value = current.ModuleId,
                    label = current.ModuleName
                }).ToList();

            module.Insert(0, new SelectViewModel()
            {
                label = "همه",
                value = 0
            });

            return module;
        }


        /// <summary>
        /// گرفتن کوئری ماژول برای لیست کشویی با اعمال دسترسی
        /// </summary>
        /// <param name="userAccess"></param>
        /// <returns></returns>
        public IEnumerable<MenuViewModel> GetAllModuleQuery(string userAccess)
        {
            // get all modules that user has access(has repetive moduleId)
            var accessModuleIds = _actions
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Module.Priority)
                .GroupBy(current => new
                {
                    current.Controller.ModuleId,
                    current.ActionBit
                })
                .Select(current => new
                {
                    current.Key.ModuleId,
                    current.Key.ActionBit
                })
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => current.ModuleId).ToList();

            // get all modules
            var allModules = _actions
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Module.Priority)
                .GroupBy(current => new
                {
                    current.Controller.ModuleId,
                    ModuleName = current.Controller.Module.Name,
                })
                .Select(current => new MenuViewModel
                {
                    ModuleId = current.Key.ModuleId,
                    ModuleName = current.Key.ModuleName,
                }).ToList();


            return allModules
                .Where(current => accessModuleIds.Contains(current.ModuleId));
        }
    }
}
