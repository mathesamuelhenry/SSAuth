using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Church.API.Models.AppModel.Request
{
    public class SignIn
    {
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
