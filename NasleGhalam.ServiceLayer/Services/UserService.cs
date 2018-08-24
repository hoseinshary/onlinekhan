using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ServiceLayer.Jwt;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.Services
{
    public class UserService
    {
        private const string Title = "کاربر";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _users;
        private readonly Lazy<RoleService> _roleService;
        private readonly Lazy<ActionService> _actionService;

        public UserService(IUnitOfWork uow,
            Lazy<RoleService> roleService,
            Lazy<ActionService> actionService)
        {
            _uow = uow;
            _users = _uow.Set<User>();
            _roleService = roleService;
            _actionService = actionService;
        }


        /// <summary>
        /// گرفتن  کاربر با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public UserGetViewModel GetById(int id, byte userRoleLevel)
        {
            return _users
                .Where(current => current.Id == id)
                .Where(current => current.Role.Level > userRoleLevel)
                .Select(current => new UserGetViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Family = current.Family,
                    Username = current.Username,
                    IsActive = current.IsActive,
                    RoleId = current.RoleId,
                    CityId = current.CityId,
                    Gender = current.Gender,
                    Mobile = current.Mobile,
                    NationalNo = current.NationalNo,
                    Phone = current.Phone,
                    ProvinceId = current.City.ProvinceId
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه کاربر ها
        /// </summary>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<UserGetViewModel> GetAll(byte userRoleLevel)
        {
            return _users
                .Where(current => current.Role.Level > userRoleLevel)
                .Select(current => new UserGetViewModel()
                {
                    Id = current.Id,
                    Name = current.Name,
                    Family = current.Family,
                    Username = current.Username,
                    IsActive = current.IsActive,
                    RoleId = current.RoleId,
                    CityId = current.CityId,
                    Gender = current.Gender,
                    Mobile = current.Mobile,
                    NationalNo = current.NationalNo,
                    Phone = current.Phone,
                    RoleName = current.Role.Name,
                    CityName = current.City.Name,
                    GenderName = current.Gender ? "پسر" : "دختر"
                }).ToList();
        }


        /// <summary>
        /// ثبت کاربر
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Create(UserCreateViewModel userViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(userViewModel.RoleId, userRoleLevel);
            if (role.Level <= userRoleLevel)
                return new MessageResult()
                {
                    FaMessage = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };

            var user = Mapper.Map<User>(userViewModel);
            user.LastLogin = DateTime.Now;
            _users.Add(user);
            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = user.Id;
            return msgRes;
        }


        /// <summary>
        /// ویرایش کاربر
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Update(UserUpdateViewModel userViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(userViewModel.RoleId, userRoleLevel);
            if (role == null)
                return new MessageResult()
                {
                    FaMessage = "نقش یافت نگردید",
                    MessageType = MessageType.Error
                };

            if (role.Level <= userRoleLevel)
                return new MessageResult()
                {
                    FaMessage = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };

            var user = Mapper.Map<UserUpdateViewModel, User>(userViewModel);
            if (string.IsNullOrEmpty(user.Password))
            {
                _uow.ExcludeFieldsFromUpdate(user, x => x.Password, x => x.LastLogin);
                _uow.ValidateOnSaveEnabled(false);
            }
            else
            {
                _uow.ExcludeFieldsFromUpdate(user, x => x.LastLogin);

            }

            return _uow.CommitChanges(CrudType.Update, Title);
        }


        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Delete(int id, byte userRoleLevel)
        {
            var userViewModel = GetById(id, userRoleLevel);
            if (userViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var user = Mapper.Map<User>(userViewModel);
            _uow.MarkAsDeleted(user);
            return _uow.CommitChanges(CrudType.Delete, Title);

        }


        /// <summary>
        /// گرفتن همه کاربر ها برای لیست کشویی
        /// </summary>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl(byte userRoleLevel)
        {
            return _users
                .Where(current => current.Role.Level > userRoleLevel)
                .Select(current => new SelectViewModel
                {
                    value = current.Id,
                    label = current.Name
                }).ToList();
        }


        /// <summary>
        /// احراز هویت
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public LoginResultViewModel Login(LoginViewModel login)
        {
            var loginResult = new LoginResultViewModel
            {
                MessageType = MessageType.Error
            };

            var lstUsr = _users
                .Include(current => current.Role)
                .Where(current => current.Username == login.UserName)
                .Where(current => current.Password == login.Password)
                .ToList();

            if (lstUsr.Count == 1)
            {
                var usr = lstUsr[0];
                if (!usr.IsActive)
                {
                    loginResult.Message = "نام کاربری شما فعال نمی باشد.";
                }
                else
                {
                    loginResult.SubMenus = _actionService.Value.GetSubMenu(usr.Role.SumOfActionBit);
                    if (loginResult.SubMenus.Count == 0)
                    {
                        loginResult.Message = "شما به صفحه ای دسترسی ندارید";
                    }
                    else
                    {
                        string defaultPage = "";
                        foreach (var item in loginResult.SubMenus)
                        {
                            if (item.EnName == "/Dashboard/Map")
                            {
                                defaultPage = item.EnName;
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(defaultPage))
                        {
                            defaultPage = loginResult.SubMenus[0].EnName;
                        }

                        loginResult.Message = "در حال انتقال...";
                        loginResult.MessageType = MessageType.Success;

                        loginResult.Menus = _actionService.Value.GetMenu(usr.Role.SumOfActionBit);
                        loginResult.DefaultPage = defaultPage;

                        loginResult.FullName = usr.Name + " " + usr.Family;
                        loginResult.Token = JsonWebToken.CreateToken(usr.Role.Level,
                            usr.IsAdmin, usr.Id, usr.Role.SumOfActionBit);
                    }
                }
            }
            else
            {
                loginResult.Message = "نام کاربری یا رمز عبور اشتباه میباشد.";
            }

            return loginResult;
        }
    }
}
