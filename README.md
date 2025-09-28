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






=====================================

extra note

####lst time

docker compose up -d db


cd server
dotnet ef migrations add InitialCreate   # only needed once
dotnet ef database update                # applies schema to SQL Server, if faced network issue, set the server=localhost,1433 , run this command and switch back to server=db


docker compose up --build


##subsequent run is 

docker compose up --build

#### when models change

dotnet ef migrations add AddOrdersTable
dotnet ef database update

docker compose up --build


#### how to run queries

## download sqlcmd, rmember to set sqlcmd in environment variable
winget install sqlcmd

### run the following to use database
sqlcmd -S localhost,1433 -U sa -P Your_password123 -d ShoppingCartDb

## list all tables in database
select name from sys.tables;
go

## run queries example below
select * from products;
go

The container didn’t rebuild with the new config (maybe Docker cached it).

Fix: run docker compose build --no-cache server then docker compose up server.

docker compose rm -s -f server  

docker compose build --no-cache server

docker compose up -d --build server

dotnet publish -c Debug
