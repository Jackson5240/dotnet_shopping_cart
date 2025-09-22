using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<bool> Register(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("api/auth/register", new { email, password });
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> Login(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", new { email, password });
        if (!res.IsSuccessStatusCode) return false;
        var payload = await res.Content.ReadFromJsonAsync<TokenResponse>();
        if (payload is null || string.IsNullOrWhiteSpace(payload.token)) return false;
        await _js.InvokeVoidAsync("authStore.setToken", payload.token);
        return true;
    }

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("authStore.clearToken");
    }

    public async Task AttachTokenAsync(HttpClient client)
    {
        var token = await _js.InvokeAsync<string?>("authStore.getToken");
        if (!string.IsNullOrWhiteSpace(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private record TokenResponse(string token);
}
