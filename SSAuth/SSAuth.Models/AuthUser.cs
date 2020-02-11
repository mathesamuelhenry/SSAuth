using System;
using System.Collections.Generic;

namespace SSAuth.Models
{
    public partial class AuthUser
    {
        public AuthUser()
        {
            LoginHistory = new HashSet<LoginHistory>();
            UserRole = new HashSet<UserRole>();
            UserSecurityQuestion = new HashSet<UserSecurityQuestion>();
        }

        public int AuthUserId { get; set; }
        public int AuthGroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string UserAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual AuthGroup AuthGroup { get; set; }
        public virtual ICollection<LoginHistory> LoginHistory { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<UserSecurityQuestion> UserSecurityQuestion { get; set; }
    }
}
