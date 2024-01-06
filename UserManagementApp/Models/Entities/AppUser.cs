using Microsoft.AspNetCore.Identity;

namespace UserManagementApp.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
