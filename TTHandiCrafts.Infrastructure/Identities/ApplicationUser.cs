using Microsoft.AspNetCore.Identity;
using System;

namespace TTHandiCrafts.Infrastructure.Identities
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime Created { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public bool NeedChangePassword { get; set; }
    }
    //public class UserPasswordHistory
    //{
    //    public int Id { get; set; }
    //    public string UserId { get; set; }
    //    public string PasswordHash { get; set; }
    //}
}
