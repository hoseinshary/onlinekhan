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
        public MessageResultClient Create(RoleViewModel roleViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ثبت کننده باشد
            if (roleViewModel.Level <= userRoleLevel)
            {
                MessageResultServer msgRes1 = new MessageResultServer()
                {
                    FaMessage = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
                return Mapper.Map<MessageResultClient>(msgRes1);
            }
            var role = Mapper.Map<Role>(roleViewModel);
            role.SumOfActionBit = "0";
            _roles.Add(role);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = role.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش نقش
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResultClient Update(RoleViewModel roleViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            if (roleViewModel.Level <= userRoleLevel)
            {
                MessageResultServer msgRes1 = new MessageResultServer()
                {
                    FaMessage = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
                return Mapper.Map<MessageResultClient>(msgRes1);
            }

            var oldRole = GetById(roleViewModel.Id, userRoleLevel);
            if (oldRole == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var newRole = Mapper.Map<Role>(roleViewModel);
            newRole.SumOfActionBit = oldRole.SumOfActionBit;

            _uow.MarkAsChanged(newRole);
            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف نقش
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id, byte userRoleLevel)
        {
            var roleViewModel = GetById(id, userRoleLevel);
            if (roleViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var role = Mapper.Map<Role>(roleViewModel);
            _uow.MarkAsDeleted(role);
            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
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
        public MessageResultClient ChangeAccess(RoleAccessViewModel roleAccess, string userAccess, byte userRoleLevel)
        {
            var roleViewModel = GetById(roleAccess.RoleId, userRoleLevel);
            var action = _actionService.Value.GetActionById(roleAccess.ActionId);

            if (roleViewModel == null || action == null)
            {
                MessageResultServer msgRes1 = new MessageResultServer()
                {
                    FaMessage = "نقش یافت نگردید.",
                    MessageType = MessageType.Error
                };
                return Mapper.Map<MessageResultClient>(msgRes1);
            }


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

            var role = Mapper.Map<Role>(roleViewModel);
            _uow.MarkAsChanged(role);
            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }
    }
}
