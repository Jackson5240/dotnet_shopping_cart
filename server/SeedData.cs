public static class SeedData
{
    public static async Task EnsureSeedAsync(AppDbContext db)
    {
        if (!db.Products.Any())
        {
            db.Products.AddRange(new[] {
                new Product { Name = "Coffee Beans", Description = "Arabica, 1kg", Price = 18.90m, ImageUrl = "" },
                new Product { Name = "Tea Sampler", Description = "Green/Black/Oolong", Price = 12.50m, ImageUrl = "" },
                new Product { Name = "French Press", Description = "1L borosilicate glass", Price = 24.00m, ImageUrl = "" },
            });
            await db.SaveChangesAsync();
        }
    }
}
