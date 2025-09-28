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

    public async Task UpdateAsync(Product product)
{
    _db.Products.Update(product);
    await _db.SaveChangesAsync();
}

public async Task DeleteAsync(Product product)
{
    _db.Products.Remove(product);
    await _db.SaveChangesAsync();
}

}
