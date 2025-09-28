using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    // this configuration settings are from appsettings.json and Environment variables
    private readonly IConfiguration _config;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
        // find the link here https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-9.0&utm_source=chatgpt.com&tabs=visual-studio --> Identity on ASP.NET Core
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) return Unauthorized();

        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized();

        var token = GenerateJwtToken(user);

        //Jackson check token
        var handler = new JwtSecurityTokenHandler();
        var checktokenOnly = handler.ReadJwtToken(token);

        foreach (var claim in checktokenOnly.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
        //Jacksone end check token
        
        return Ok(new { token });
    }

//https://www.ais.com/how-to-generate-a-jwt-token-using-net-6/?utm_source=chatgpt.com
//https://www.c-sharpcorner.com/article/jwt-token-creation-authentication-and-authorization-in-asp-net-core-6-0-with-po/?utm_source=chatgpt.com
    private string GenerateJwtToken(ApplicationUser user)
    {
        // If it’s missing, the ?? (null-coalescing operator) provides a fallback string:
        //Converts the key string into a byte[]. --> Encoding.UTF8.GetBytes
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "super_secret_dev_key_change_me"));
        //the jwt will be signed using MAC with SHA-256
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //So when the client presents the token later, the server can read the claims and know who the user is, their email and jti
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            //If Email is null, it falls back to an empty string.
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            //jti = "JWT ID" (a unique identifier for this token).
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        // creds → signs the token with your secret key + HMAC SHA-256 algorithm, so it can’t be tampered with.
        //optional can add this in production, issuer: "your-app", audience: "your-client-app"
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);
        //convert token string into the compact JWT string format
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

//DTO stands for Data Transfer Object
//record = special C# type (like a class) designed for immutable data objects.
public record RegisterDto(string Email, string Password);
/* Above same as below
public class RegisterDto
{
    public string Email { get; init; }
    public string Password { get; init; }

    public RegisterDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    // `record` also auto-generates useful stuff like:
    // - Value equality (compares properties, not object references)
    // - A good ToString()
    // - Deconstruct() method
}
*/
public record LoginDto(string Email, string Password);
