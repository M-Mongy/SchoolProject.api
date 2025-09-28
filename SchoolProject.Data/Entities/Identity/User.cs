using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity
{
    // Inherit from IdentityUser
    public class User : IdentityUser<int>
    {
        // You can still add your custom properties here
        public string? FullName { get; set; }
        public string? Address { get; set; }
        // etc.
    }
}