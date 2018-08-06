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
        private readonly short[] _actionBits;
        public CheckUserAccess(params ActionBits[] actionBits)
        {
            _actionBits = actionBits.Select(current => (short)current).ToArray();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //return;
            bool isAuthenticated = false;

            string token = null;
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("Token", out values))
            {
                token = values.FirstOrDefault();
            }

            ////todo: remove later, for test
            if (token == null)
                token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJWYWx1ZSI6IjFfVHJ1ZV8xIiwiQWNjZXNzIjoiMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTEwIiwiRXhwIjo2MzY2OTMyNDczMTk0NzE0NTl9.eniF9VrmRZ8TQTS-0o7uAIX0ufAOK-ZIwZS6fXq3SfKjcA6Nq1g4zFaZKudTCzPVabh-ypxUBA0R_aP0O-6GXA";

            if (token != null)
            {
                try
                {
                    // decode token and convert to JwtPayload
                    var jsonPayload = JsonWebToken.Decode(token);
                    long tick = DateTime.Now.ToUniversalTime().Ticks;

                    //if token does not expire
                    if (jsonPayload.Exp > tick)
                    {
                        // if public action 
                        if (_actionBits == null ||
                            _actionBits.Length == 0 ||
                            _actionBits[0] == (short)ActionBits.PublicAcceess)
                        {
                            isAuthenticated = true;
                        }
                        // if token has access to this action
                        else if (Utility.HasAccess(jsonPayload.Access, _actionBits))
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