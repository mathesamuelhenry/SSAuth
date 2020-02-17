using System;
using System.Collections.Generic;
using System.Text;

namespace SSAuth.Models.AppModels.Response.AuthUser
{
    public class UserRoleResponse
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
