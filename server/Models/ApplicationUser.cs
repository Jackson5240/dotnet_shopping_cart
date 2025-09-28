using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Extend with profile fields if desired
    //inherited Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed
    //SecurityStamp, ConcurrencyStamp, LockoutEnd, LockoutEnabled, AccessFailedCount

    /*
    If wanna add can add like this 
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Department { get; set; } = string.Empty;

    */
}
