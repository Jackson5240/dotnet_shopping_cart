using System.Net.Http.Json;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;
    public ApiClient(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }
/*
    public async Task<List<Product>> GetProducts() =>
        await _http.GetFromJsonAsync<List<Product>>("api/products") ?? new();

    public async Task AddToCart(int productId, int qty)
    {
        await _auth.AttachTokenAsync(_http);
        var res = await _http.PostAsJsonAsync("api/cart/add", new { ProductId = productId, Quantity = qty });
        res.EnsureSuccessStatusCode();
    }

        public async Task<List<CartItem>> GetCart()
    {
        await _auth.AttachTokenAsync(_http);
        return await _http.GetFromJsonAsync<List<CartItem>>("api/cart") ?? new();
    }
*/

    public async Task<List<Product>> GetProducts() =>
        await _http.GetFromJsonAsync<List<Product>>("products") ?? new();

    public async Task AddToCart(int productId, int qty)
    {
        await _auth.AttachTokenAsync(_http);
        var res = await _http.PostAsJsonAsync("cart/add", new { ProductId = productId, Quantity = qty });
        res.EnsureSuccessStatusCode();
    }

    public async Task<List<CartItem>> GetCart()
    {
        await _auth.AttachTokenAsync(_http);
        return await _http.GetFromJsonAsync<List<CartItem>>("cart") ?? new();
    }
}
