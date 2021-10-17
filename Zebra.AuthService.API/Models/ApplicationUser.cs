using Microsoft.AspNetCore.Identity;
using System;

namespace Zebra.AuthService.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsCustomerCreated { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
