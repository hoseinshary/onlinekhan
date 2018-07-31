﻿using System;
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

            //string token = null;
            //IEnumerable<string> values;
            //if (actionContext.Request.Headers.TryGetValues("Token", out values))
            //{
            //    token = values.FirstOrDefault();
            //}

            //todo: remove later, for test
            var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJWYWx1ZSI6IjFfVHJ1ZV8xIiwiQWNjZXNzIjoiMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTEwIiwiRXhwIjo2MzY2OTAwNjYwNDc1NDQ5MjR9._lWuHN-3bgCez5aV_MQssQpPoI073q_L1uwhIScf_7Lkfhnb9y2rERQDyXE1tnvBodQIVvmfpG_qZ-nj8ITB0Q";

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
                        if (_actionBits == null || _actionBits.Length == 0) 
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