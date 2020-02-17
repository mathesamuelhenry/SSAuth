using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSAuth.Models.AppModels.Request.AuthUser
{
    public class ChangePassword
    {
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}
