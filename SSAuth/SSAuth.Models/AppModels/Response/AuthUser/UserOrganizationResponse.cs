using System;
using System.Collections.Generic;
using System.Text;

namespace SSAuth.Models.AppModels.Response.AuthUser
{
    public class UserOrganizationResponse
    {
        public int UserOrganizationId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}
