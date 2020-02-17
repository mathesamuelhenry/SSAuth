using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSAuth.Models.AppModels.Request.AuthUser
{
    public class ChangePasswordFromOriginal : ChangePassword
    {
        [Required]
        public string OriginalPassword { get; set; }
    }
}
