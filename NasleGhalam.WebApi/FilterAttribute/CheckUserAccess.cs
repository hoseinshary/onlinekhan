using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Jwt;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckUserAccess : ActionFilterAttribute
    {
        public short[] ActionBit { set; get; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            return;
            bool isAuthenticated = false;

            string token = null;
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("Token", out values))
            {
                token = values.FirstOrDefault();
            }

            if (token != null)
            {
                try
                {
                    // if valid token...
                    var jsonPayload = JsonWebToken.Decode(token);
                    long tick = DateTime.Now.ToUniversalTime().Ticks;

                    //if token does not expire
                    if (jsonPayload.Exp > tick)
                    {
                        if (ActionBit.Any()) // if public action 
                        {
                            isAuthenticated = true;
                        }
                        else if (Utility.HasAccess(jsonPayload.Access, ActionBit)) // if token has access to this action
                        {
                            isAuthenticated = true;
                        }
                        //else
                        //{
                        //    isAuthenticated = false;
                        //}

                        if (isAuthenticated)
                        {
                            var lst = jsonPayload.Value.Split('_');

                            actionContext.Request.Properties.Add("_roleLevel", lst[0]);
                            actionContext.Request.Properties.Add("_isAdmin", lst[1]);
                            actionContext.Request.Properties.Add("_user_id", lst[2]);

                            actionContext.Request.Properties.Add("_access", jsonPayload.Access);
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //ex.ToString();
                    isAuthenticated = false;
                }
            }

            if (!isAuthenticated)
            {
                actionContext.Response = actionContext.ControllerContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "عدم دسترسی");
                //.CreateResponse(HttpStatusCode.Unauthorized, Utility.UnauthorizedMessage());
            }
        }
    }
}