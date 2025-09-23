using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync() => await _db.Products.ToListAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);
    public async Task AddAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
    }
}
