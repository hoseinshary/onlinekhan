using System;
using System.Net.Http;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.Extensions
{
    public static class RequestExtension
    {
        public static int GetUserId(this HttpRequestMessage request)
        {
            var obj = request.Properties["_user_id"];
            return Convert.ToInt32(obj);
        }

        public static bool GetIsAdmin(this HttpRequestMessage request)
        {
            var obj = request.Properties["_isAdmin"];
            return Convert.ToBoolean(obj);
        }

        public static string GetAccess(this HttpRequestMessage request)
        {
            var obj = request.Properties["_access"];
            return Convert.ToString(obj);
        }

        public static byte GetRoleLevel(this HttpRequestMessage request)
        {
            var obj = request.Properties["_roleLevel"];
            return Convert.ToByte(obj);
        }

        public static UserType GetUserType(this HttpRequestMessage request)
        {
            var obj = request.Properties["_userType"];
            var userType = Convert.ToInt32(obj);
            return (UserType)Enum.ToObject(typeof(UserType), userType);
        }
    }
}