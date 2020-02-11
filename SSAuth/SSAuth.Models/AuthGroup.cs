using System;
using System.Collections.Generic;

namespace SSAuth.Models
{
    public partial class AuthGroup
    {
        public AuthGroup()
        {
            AuthUser = new HashSet<AuthUser>();
        }

        public int AuthGroupId { get; set; }
        public int AuthMethodId { get; set; }
        public string AuthGroupName { get; set; }
        public string UserAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual AuthMethod AuthMethod { get; set; }
        public virtual ICollection<AuthUser> AuthUser { get; set; }
    }
}
