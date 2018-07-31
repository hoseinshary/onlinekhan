using System;
using System.Net.Http;

namespace NasleGhalam.WebApi.Extentions
{
    public static class RequestExtention
    {
        public static int GetUserId(this HttpRequestMessage request)
        {
            Object obj = request.Properties["_user_id"];
            return Convert.ToInt32(obj);
        }

        public static bool GetIsAdmin(this HttpRequestMessage request)
        {
            Object obj = request.Properties["_isAdmin"];
            return Convert.ToBoolean(obj);
        }

        public static String GetAccess(this HttpRequestMessage request)
        {
            Object obj = request.Properties["_access"];
            return Convert.ToString(obj);
        }

        public static byte GetRoleLevel(this HttpRequestMessage request)
        {
            Object obj = request.Properties["_roleLevel"];
            return Convert.ToByte(obj);
        }
    }
}