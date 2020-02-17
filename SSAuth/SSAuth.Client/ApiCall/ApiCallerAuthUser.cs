using SSAuth.Models.AppModels.Request.AuthUser;
using SSAuth.Models.AppModels.Response.AuthUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSAuth.Client.ApiCall
{
    public class ApiCallerAuthUser : ApiCallerBase
    {
        public ApiCallerAuthUser(string uri) :
            base(uri)
        {

        }

        public GetUserResponse RegisterUser(Register userRequest)
        {
            string url = $"/api/AuthUsers/Register";

            var userInfo = ApiHelper.CallPostWebApi<Register, GetUserResponse>(url, userRequest);

            return userInfo;
        }
    }
}
