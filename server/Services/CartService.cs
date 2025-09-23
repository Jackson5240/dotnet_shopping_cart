using Microsoft.EntityFrameworkCore;

public class CartService
{
    private readonly AppDbContext _db;
    public CartService(AppDbContext db) => _db = db;

    public async Task<List<CartItem>> GetUserCartAsync(string userId) =>
        await _db.CartItems.Where(c => c.UserId == userId).Include(c => c.Product).ToListAsync();

    public async Task AddToCartAsync(string userId, int productId, int quantity)
    {
        var existing = await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        if (existing != null)
            existing.Quantity += quantity;
        else
            _db.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, UserId = userId });

        await _db.SaveChangesAsync();
    }
}
