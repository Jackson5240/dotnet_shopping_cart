using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        await _service.AddAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT: api/products/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        var existing = await _service.GetByIdAsync(id);
        if (existing is null)
            return NotFound();

        // update fields
        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.Description = product.Description;

        await _service.UpdateAsync(existing);
        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        await _service.DeleteAsync(product);
        return NoContent();
    }
}
