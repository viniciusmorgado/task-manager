#nullable disable
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities;

public class AppUser : IdentityUser
{
    public string Username { get; set; }
}
