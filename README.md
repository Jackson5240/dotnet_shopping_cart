# Shopping Cart (.NET 9)

## Quick start
```bash
# Server
cd server
dotnet restore
dotnet run
# or for DB creation with migrations later: dotnet ef database update
```

```bash
# Client (Blazor WASM)
cd client
dotnet restore
dotnet run  # launches dev server
```

Open:
- Client: http://localhost:5000
- Server: http://localhost:5001/swagger
```

### Default URLs
- Products: GET /api/products
- Cart (auth required): GET /api/cart, POST /api/cart/add
- Auth: POST /api/auth/register, POST /api/auth/login (returns { token })
```

### Notes
- JWT token is stored in `localStorage` on the client.
- Update `appsettings.json` `Jwt:Key` for production.
- Replace SQLite with SQL Server/Postgres by swapping EF provider.
