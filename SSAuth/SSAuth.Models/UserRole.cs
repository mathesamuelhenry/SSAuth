using System;
using System.Collections.Generic;

namespace SSAuth.Models
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int AuthUserId { get; set; }
        public int RoleId { get; set; }
        public string UserAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual AuthUser AuthUser { get; set; }
        public virtual Role Role { get; set; }
    }
}
