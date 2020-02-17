using System;
using System.Collections.Generic;
using System.Text;
using AuthModels = SSAuth.Models;

namespace SSAuth.Models.AppModels.Response.AuthUser
{
    public class GetUserResponse
    {
        public GetUserResponse()
        {

        }

        public GetUserResponse(AuthModels.AuthUser authUser)
        {
            AuthUserId = authUser.AuthUserId;
            FirstName = authUser.FirstName;
            LastName = authUser.LastName;
            EmailId = authUser.Email;
            LoginId = authUser.LoginId;
            UserStatus = authUser.Status;
        }

        public int AuthUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserStatus { get; set; }
        public string EmailId { get; set; }
        public string LoginId { get; set; }
        public List<UserRoleResponse> UserRole { get; set; }
        public List<UserOrganizationResponse> UserOrganization { get; set; }
    }
}
