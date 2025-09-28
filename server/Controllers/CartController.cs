using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly AppDbContext _db;
    public CartController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetUserCart()
    {
        var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Unauthorized();

        var items = await _db.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        return Ok(items);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(AddToCartDto dto)
    {
        //Jackson added

        //Jackson added end
        //var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User?.FindFirst("sub")?.Value;
        if (userId is null) return Unauthorized();

        var existing = await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == dto.ProductId);
        if (existing is not null) existing.Quantity += dto.Quantity;
        else _db.CartItems.Add(new CartItem { ProductId = dto.ProductId, Quantity = dto.Quantity, UserId = userId });

        await _db.SaveChangesAsync();
        return Ok();
    }
}

public record AddToCartDto(int ProductId, int Quantity);
