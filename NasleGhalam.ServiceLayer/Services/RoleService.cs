using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Role;

namespace NasleGhalam.ServiceLayer.Services
{
    public class RoleService
    {
        private const string Title = "نقش";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Role> _roles;
        private readonly Lazy<ActionService> _actionService;

        public RoleService(IUnitOfWork uow, Lazy<ActionService> actionService)
        {
            _uow = uow;
            _roles = uow.Set<Role>();
            _actionService = actionService;
        }


        /// <summary>
        /// گرفتن  نقش با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public RoleViewModel GetById(int id, byte userRoleLevel)
        {
            return _roles
                .Where(current => current.Id == id)
                .Where(current => current.Level > userRoleLevel)
                .Select(current => new RoleViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Level = current.Level,
                    SumOfActionBit = current.SumOfActionBit
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه نقش ها
        /// </summary>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<RoleViewModel> GetAll(byte userRoleLevel)
        {
            return _roles
                .Where(current => current.Level > userRoleLevel)
                .Select(current => new RoleViewModel()
                {
                    Id = current.Id,
                    Name = current.Name,
                    Level = current.Level
                }).ToList();
        }


        /// <summary>
        /// ثبت نقش
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Create(RoleViewModel roleViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ثبت کننده باشد
            if (roleViewModel.Level <= userRoleLevel)
                return new MessageResult()
                {
                    FaMessage = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };

            var role = Mapper.Map<Role>(roleViewModel);
            role.SumOfActionBit = "0";
            _roles.Add(role);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = role.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش نقش
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Update(RoleViewModel roleViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            if (roleViewModel.Level <= userRoleLevel)
                return new MessageResult()
                {
                    FaMessage = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };

            var oldRole = GetById(roleViewModel.Id, userRoleLevel);
            if (oldRole == null)
            {
                return Utility.NotFoundMessage();
            }

            var newRole = Mapper.Map<Role>(roleViewModel);
            newRole.SumOfActionBit = oldRole.SumOfActionBit;

            _uow.MarkAsChanged(newRole);
            return _uow.CommitChanges(CrudType.Update, Title);
        }


        /// <summary>
        /// حذف نقش
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Delete(int id, byte userRoleLevel)
        {
            var roleViewModel = GetById(id, userRoleLevel);
            if (roleViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var role = Mapper.Map<Role>(roleViewModel);
            _uow.MarkAsDeleted(role);
            return _uow.CommitChanges(CrudType.Delete, Title);
        }


        /// <summary>
        /// گرفتن همه نقش ها برای لیست کشویی
        /// </summary>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl(byte userRoleLevel)
        {
            return _roles
                .Where(current => current.Level > userRoleLevel)
                .Select(current => new SelectViewModel
                {
                    value = current.Id,
                    label = current.Name
                }).ToList();
        }


        /// <summary>
        /// ویرایش دسترسی
        /// </summary>
        /// <param name="roleAccess"></param>
        /// <param name="userAccess"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult ChangeAccess(RoleAccessViewModel roleAccess, string userAccess, byte userRoleLevel)
        {
            var roleViewModel = GetById(roleAccess.RoleId, userRoleLevel);
            var actions = _actionService.Value.GetActionByIds(roleAccess.LstActionId);

            if (roleViewModel == null)
                return new MessageResult()
                {
                    FaMessage = "نقش یافت نگردید.",
                    MessageType = MessageType.Error
                };

            foreach (var action in actions)
            {
                // اگر در کلاینت چک خورده باشد ولی در دیتابیس چک نخورده باشد
                // باید به دسترسی آن اضاف کنیم
                if (roleAccess.IsChecked && Utility.HasAccess(userAccess, action.ActionBit))
                {
                    roleViewModel.SumOfActionBit = Utility.AddAccess(roleViewModel.SumOfActionBit, action.ActionBit);
                }
                // اگر در کلاینت چک نخورده باشد ولی در دیتابیس چک خورده باشد
                // باید از دسترسی آن کم کنیم
                else if (Utility.HasAccess(userAccess, action.ActionBit))
                {
                    roleViewModel.SumOfActionBit = Utility.RemoveAccess(roleViewModel.SumOfActionBit, action.ActionBit);

                }
            }

            var role = Mapper.Map<Role>(roleViewModel);
            _uow.MarkAsChanged(role);
            return _uow.CommitChanges(CrudType.Update, Title);
        }
    }
}
