using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ServiceLayer.Jwt;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.User;

namespace Matin.Abfa.ServiceLayer.Services
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
        public UserViewModel GetById(int id, byte userRoleLevel)
        {
            return _users
                .Where(current => current.Id == id)
                .Where(current => current.Role.Level > userRoleLevel)
                .Select(current => new UserViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Family = current.Family,
                    Username = current.Username,
                    Password = current.Password,
                    IsActive = current.IsActive,
                    RoleId = current.RoleId,
                    CityId = current.CityId,
                    Gender = current.Gender,
                    Mobile = current.Mobile,
                    NationalNo = current.NationalNo,
                    Phone = current.Phone
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه کاربر ها
        /// </summary>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<UserViewModel> GetAll(byte userRoleLevel)
        {
            return _users
                .Where(current => current.Role.Level > userRoleLevel)
                .Select(current => new UserViewModel()
                {
                    Id = current.Id,
                    Name = current.Name,
                    Family = current.Family,
                    Username = current.Username,
                    Password = current.Password,
                    IsActive = current.IsActive,
                    RoleId = current.RoleId,
                    CityId = current.CityId,
                    Gender = current.Gender,
                    Mobile = current.Mobile,
                    NationalNo = current.NationalNo,
                    Phone = current.Phone,
                    CityName = current.City.Name,
                    RoleName = current.Role.Name
                }).ToList();
        }


        /// <summary>
        /// ثبت کاربر
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public MessageResult Create(UserViewModel userViewModel, byte userRoleLevel)
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
        public MessageResult Update(UserViewModel userViewModel, byte userRoleLevel)
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

            var user = Mapper.Map<User>(userViewModel);
            _uow.MarkAsChanged(user);


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
                    var lstAccessPage = _actionService.Value.GetMenu(usr.Role.SumOfActionBit);
                    if (lstAccessPage.Count == 0)
                    {
                        loginResult.Message = "شما به صفحه ای دسترسی ندارید";
                    }
                    else
                    {
                        string defaultPage = "";
                        foreach (var item in lstAccessPage)
                        {
                            if (item.EnName == "/Dashboard/Map")
                            {
                                defaultPage = item.EnName;
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(defaultPage))
                        {
                            defaultPage = lstAccessPage[0].EnName;
                        }

                        loginResult.Message = "در حال انتقال...";
                        loginResult.MessageType = MessageType.Success;
                        loginResult.Token = JsonWebToken.CreateToken(usr.Role.Level, 
                            usr.IsAdmin, usr.Id, usr.Role.SumOfActionBit);
                        loginResult.DefaultPage = defaultPage;
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
