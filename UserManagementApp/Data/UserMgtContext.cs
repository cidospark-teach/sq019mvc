using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Models.Entities;

namespace UserManagementApp.Data
{
    public class UserMgtContext : IdentityDbContext<AppUser>
    {
        public UserMgtContext(DbContextOptions<UserMgtContext> options): base(options) { }
    }
}
