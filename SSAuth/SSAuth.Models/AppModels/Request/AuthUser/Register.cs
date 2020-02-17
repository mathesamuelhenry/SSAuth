using System;
using System.Collections.Generic;
using System.Text;

namespace SSAuth.Models.AppModels.Request.AuthUser
{
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string UserAdded { get; set; }
    }
}
