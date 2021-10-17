using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zebra.AuthService.API.Models;

namespace Zebra.AuthService.API.Persistance
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        { 
        }

        public AuthDbContext()
        {
        }
    }
}
