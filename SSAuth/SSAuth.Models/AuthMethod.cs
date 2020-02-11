using System;
using System.Collections.Generic;

namespace SSAuth.Models
{
    public partial class AuthMethod
    {
        public AuthMethod()
        {
            AuthGroup = new HashSet<AuthGroup>();
        }

        public int AuthMethodId { get; set; }
        public string AuthMethodName { get; set; }
        public string UserAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual ICollection<AuthGroup> AuthGroup { get; set; }
    }
}
