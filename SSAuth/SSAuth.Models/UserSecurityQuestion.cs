using System;
using System.Collections.Generic;

namespace SSAuth.Models
{
    public partial class UserSecurityQuestion
    {
        public int UserSecurityQuestionId { get; set; }
        public int AuthUserId { get; set; }
        public int SecurityQuestionId { get; set; }
        public string Answer { get; set; }
        public string UserAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual AuthUser AuthUser { get; set; }
        public virtual SecurityQuestion SecurityQuestion { get; set; }
    }
}
