using System;
using System.Collections.Generic;
using System.Text;

namespace SSAuth.Models.AppModels.Request.AuthUser
{
    public class ChangePasswordByQuestion : ChangePassword
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
