using System;
using System.Collections.Generic;

namespace SSAuth.Models
{
    public partial class LoginHistory
    {
        public int LoginHistoryId { get; set; }
        public int AuthUserId { get; set; }
        public string LoginId { get; set; }
        public DateTime? LoginDate { get; set; }
        public string UserAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual AuthUser AuthUser { get; set; }
    }
}
