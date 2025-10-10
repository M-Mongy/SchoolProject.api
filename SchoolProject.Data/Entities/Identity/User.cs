using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using EntityFrameworkCore.EncryptColumn.Attribute;

namespace SchoolProject.Data.Entities.Identity
{
    // Inherit from IdentityUser
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
        public string FullName { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        // etc.
    }
}