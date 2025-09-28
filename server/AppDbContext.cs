using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//IdentityDbContext is a special DbContext that already defines all the tables for ASP.NET Identity (AspNetUsers, AspNetRoles, AspNetUserClaims, etc.).
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
//Constructor that takes configuration options (e.g., which DB provider, connection string).
//The : base(options) passes those options to the parent IdentityDbContext.
//This allows you to register it in Dependency Injection:
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //The => Set<T>() syntax is shorthand for property initialization (instead of writing { get; set; }).
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
}
